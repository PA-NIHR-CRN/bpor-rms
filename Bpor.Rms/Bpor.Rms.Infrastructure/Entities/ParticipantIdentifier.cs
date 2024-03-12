using System.ComponentModel.DataAnnotations;
using Bpor.Rms.Infrastructure.Entities.RefData;

namespace Bpor.Rms.Infrastructure.Entities;


public class ParticipantIdentifier
{
    public ParticipantIdentifier()
    {
        Type = null!;
        Participant = null!;
    }

    public int Id { get; set; }
    public int ParticipantId { get; set; }

    [Required]
    public Guid Value { get; set; }

    public int IdentifierTypeId { get; set; }
    public bool IsDeleted { get; set; }

    public IdentifierType Type { get; set; }
    public Participant Participant { get; set; }
}
