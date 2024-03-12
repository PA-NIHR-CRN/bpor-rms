using Amazon;
using Amazon.SecretsManager;
using Bpor.Rms.Infrastructure.Extensions;
using Bpor.Rms.Infrastructure.Settings;

namespace Bpor.Rms.Web.Startup;

public static class ConfigurationSetup
{
    public static void AddApplicationConfiguration(this ConfigurationManager configuration)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true);

        var secretsManagerSettings = configuration.GetSection("AwsSecretsManager").Get<AwsSecretsManagerSettings>();
        if (secretsManagerSettings is { Enabled: true })
        {
            configuration.AddAwsSecretsManager(secretsManagerSettings.SecretName,
                () => new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(secretsManagerSettings.Region)));
        }
    }
}
