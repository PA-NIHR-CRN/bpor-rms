using Bpor.Rms.Infrastructure.Interfaces;
using Bpor.Rms.Infrastructure.Services;
using Bpor.Rms.Infrastructure.Settings;

namespace Bpor.Rms.Web.Startup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddOptions<IdentityProviderApiSettings>().BindConfiguration("IdentityProviderAPI")
            .ValidateDataAnnotations().ValidateOnStart();
        services.AddOptions<AuthenticationSettings>().BindConfiguration("Authentication").ValidateDataAnnotations()
            .ValidateOnStart();

        var baseAddress = configuration.GetValue<string>("IdentityProviderApi:BaseUrl") ??
                          throw new InvalidOperationException(
                              "Failed to read required IdP API BaseUrl property from configuration.");
        
        services.AddTransient<IIdentityProviderService, Wso2IdentityServerService>();
        services.AddHttpClient<IIdentityProviderService, Wso2IdentityServerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });

        services.AddDistributedMemoryCache();
    }
    
}
