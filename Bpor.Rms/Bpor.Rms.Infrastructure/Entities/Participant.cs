using System.ComponentModel.DataAnnotations;
using Bpor.Rms.Infrastructure.Entities.RefData;

namespace Bpor.Rms.Infrastructure.Entities;

public class Participant
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? FirstName { get; set; }

    [MaxLength(255)]
    public string? LastName { get; set; }

    public bool RegistrationConsent { get; set; }
    public DateTime? RegistrationConsentAtUtc { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(255)]
    public string? EthnicBackground { get; set; }
    [MaxLength(255)]
    public string? EthnicGroup { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? RemovalOfConsentRegistrationAtUtc { get; set; }
    public bool? HasLongTermCondition { get; set; }

    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    [MaxLength(255)]
    public string? MobileNumber { get; set; }
    [MaxLength(255)]
    public string? LandlineNumber { get; set; }
    [MaxLength(255)]
    public string? NHSNumber { get; set; }

    public bool IsDeleted { get; set; }
    public int? DailyLifeImpactId { get; set; }
    public int? CommunicationLanguageId { get; set; }
    public int? GenderId { get; set; }

    public DailyLifeImpact DailyLifeImpact { get; set; }
    public CommunicationLanguage CommunicationLanguage { get; set; }
    public Gender Gender { get; set; }
    public ICollection<ParticipantHealthCondition> HealthConditions { get; set; } =
        new List<ParticipantHealthCondition>();
    public ICollection<ParticipantIdentifier> ParticipantIdentifiers { get; set; } =
        new List<ParticipantIdentifier>();
    public ParticipantAddress? Address { get; set; }

    public ICollection<SourceReference> SourceReferences { get; set; } = new List<SourceReference>();
}
