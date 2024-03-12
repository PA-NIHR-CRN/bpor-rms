using System.ComponentModel.DataAnnotations;

namespace Bpor.Rms.Infrastructure.Entities;

public class SourceReference
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string Pk { get; set; } = null!;
    public int ParticipantId { get; set; }

    public Participant Participant { get; set; } = null!;
}
