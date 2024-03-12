using System.ComponentModel.DataAnnotations;
using MySqlConnector;

namespace Bpor.Rms.Infrastructure.Settings;

public class DbSettings : IValidatableObject
{
    public const string SectionName = "DbSettings";
    [Required] public string Username { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string Host { get; set; } = null!;
    [Required] public int Port { get; set; }
    [Required] public string Database { get; set; } = null!;

    public string BuildConnectionString()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = (uint)Port,
            UserID = Username,
            Password = Password,
            Database = Database,
        };
        return connectionStringBuilder.ConnectionString;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            yield return new ValidationResult("Username is required", new[] { nameof(Username) });
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            yield return new ValidationResult("Password is required", new[] { nameof(Password) });
        }

        if (string.IsNullOrWhiteSpace(Host))
        {
            yield return new ValidationResult("Host is required", new[] { nameof(Host) });
        }

        if (Port <= 0)
        {
            yield return new ValidationResult("Port is required", new[] { nameof(Port) });
        }

        if (string.IsNullOrWhiteSpace(Database))
        {
            yield return new ValidationResult("Database is required", new[] { nameof(Database) });
        }
    }
}
