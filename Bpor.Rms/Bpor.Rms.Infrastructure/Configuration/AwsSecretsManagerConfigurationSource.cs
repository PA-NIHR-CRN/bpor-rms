using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Bpor.Rms.Infrastructure.Configuration;

public class AwsSecretsManagerConfigurationSource(
    string secretName,
    Func<IAmazonSecretsManager> secretsManagerClientFactory)
    : JsonStreamConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AwsSecretsManagerConfigurationProvider(this, secretName, secretsManagerClientFactory);
    }
}
