namespace Bpor.Rms.Infrastructure.Interfaces;

public interface IIdentityProviderService
{
    Task<string> GetOrAcquireTokenAsync(CancellationToken cancellationToken = default);
}
