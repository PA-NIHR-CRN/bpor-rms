using System.Net.Http.Json;
using Bpor.Rms.Infrastructure.DTOs;
using Bpor.Rms.Infrastructure.Interfaces;
using Bpor.Rms.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Bpor.Rms.Infrastructure.Services;

public class Wso2IdentityServerService(
    IDistributedCache tokenCache,
    IOptions<IdentityProviderApiSettings> config,
    HttpClient httpClient)
    : IIdentityProviderService
{
    private const string TokenCacheKey = "api_bearer_token";

    public async Task<string> GetOrAcquireTokenAsync(CancellationToken cancellationToken = default)
    {
        var cachedToken = await tokenCache.GetStringAsync(TokenCacheKey, cancellationToken);
        if (string.IsNullOrWhiteSpace(cachedToken))
        {
            var authenticationResponse = await httpClient.PostAsync(
                "/oauth2/token",
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "client_id", config.Value.ClientId },
                        { "client_secret", config.Value.ClientSecret },
                        { "grant_type", "client_credentials" },
                        { "scope", "internal_user_mgt_view internal_user_mgt_list" },
                    }), cancellationToken);

            if (!authenticationResponse.IsSuccessStatusCode)
            {
                var errorContent = await authenticationResponse.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException(
                    $"Failed to acquire token from WSO2. Status: {authenticationResponse.StatusCode}, Body: {errorContent}");
            }

            var tokenResponse =
                await authenticationResponse.Content
                    .ReadFromJsonAsync<Wso2AuthenticationResponseDto>(cancellationToken);
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("Invalid token response from identity provider.");
            }

            if (tokenResponse.ExpiresIn > 120)
            {
                // Subtracting a buffer from expiresIn to ensure token is refreshed before expiry
                await tokenCache.SetStringAsync(
                    TokenCacheKey,
                    tokenResponse.AccessToken,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 120)
                    }, cancellationToken);
            }

            return tokenResponse.AccessToken;
        }

        return cachedToken ??
               throw new InvalidOperationException("Identity Provider API token found in cache was null.");
    }
}
