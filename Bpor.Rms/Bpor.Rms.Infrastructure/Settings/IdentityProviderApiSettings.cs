using System.ComponentModel.DataAnnotations;

namespace Bpor.Rms.Infrastructure.Settings;

public class IdentityProviderApiSettings : IValidatableObject
{
    [Required] public string BaseUrl { get; set; } = string.Empty;
    [Required] public string ClientId { get; set; } = string.Empty;
    [Required] public string ClientSecret { get; set; } = string.Empty;


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            yield return new ValidationResult("BaseUrl is required", new[] { nameof(BaseUrl) });
        }

        if (string.IsNullOrWhiteSpace(ClientId))
        {
            yield return new ValidationResult("ClientId is required", new[] { nameof(ClientId) });
        }

        if (string.IsNullOrWhiteSpace(ClientSecret))
        {
            yield return new ValidationResult("ClientSecret is required", new[] { nameof(ClientSecret) });
        }
    }
}
