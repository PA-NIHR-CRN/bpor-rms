using System.ComponentModel.DataAnnotations;

namespace Bpor.Rms.Infrastructure.Entities;

public class ParticipantAddress
{
    public ParticipantAddress()
    {
        Participant = null!;
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? AddressLine1 { get; set; }
    [MaxLength(255)]
    public string? AddressLine2 { get; set; }
    [MaxLength(255)]
    public string? AddressLine3 { get; set; }
    [MaxLength(255)]
    public string? AddressLine4 { get; set; }
    [MaxLength(255)]
    public string? Town { get; set; }
    [MaxLength(255)]
    public string? Postcode { get; set; }

    public int ParticipantId { get; set; }

    [Required]
    public Participant Participant { get; set; }
    public bool IsDeleted { get; set; }
}
