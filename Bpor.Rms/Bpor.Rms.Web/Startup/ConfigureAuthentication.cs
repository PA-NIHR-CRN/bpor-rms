using Bpor.Rms.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Bpor.Rms.Web.Startup;

public static class ConfigureAuthentication
{
    public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = authenticationSettings.Authority;
                options.ClientId = authenticationSettings.ClientId;
                options.ClientSecret = authenticationSettings.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.CallbackPath = "/signin-oidc";
            });
    }
}
