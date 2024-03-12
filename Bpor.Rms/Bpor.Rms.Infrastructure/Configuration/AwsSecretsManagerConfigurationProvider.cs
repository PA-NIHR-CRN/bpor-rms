using System.Text;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration.Json;

namespace Bpor.Rms.Infrastructure.Configuration;

public class AwsSecretsManagerConfigurationProvider(
    JsonStreamConfigurationSource source,
    string secretName,
    Func<IAmazonSecretsManager> secretsManagerClientFactory)
    : JsonStreamConfigurationProvider(source)
{
    public override void Load()
    {
        var secretsManager = secretsManagerClientFactory.Invoke();

        var response = secretsManager.GetSecretValueAsync(new GetSecretValueRequest { SecretId = secretName })
            .GetAwaiter().GetResult();

        var jsonStream = response.SecretBinary;

        if (response.SecretString != null)
        {
            jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(response.SecretString));
        }

        if (jsonStream != null)
        {
            base.Load(jsonStream); // Stream will be disposed correctly in here.
        }
        else
        {
            throw new AmazonSecretsManagerException($"Failed to load Secrets Manager secret with key {secretName}.");
        }
    }
}
