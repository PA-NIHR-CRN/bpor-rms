using Amazon.SecretsManager;
using Bpor.Rms.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Bpor.Rms.Infrastructure.Extensions;

public static class ConfigExtensions
{
    public static void AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder, string secretName,
        Func<IAmazonSecretsManager> secretsManagerClientFactory)
    {
        var configurationSource = new AwsSecretsManagerConfigurationSource(secretName, secretsManagerClientFactory);
        configurationBuilder.Add(configurationSource);
    }
}
