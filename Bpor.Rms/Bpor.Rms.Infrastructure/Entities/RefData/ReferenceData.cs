using System.ComponentModel.DataAnnotations;

namespace Bpor.Rms.Infrastructure.Entities.RefData;

public abstract class ReferenceData : IReferenceData
{
    protected ReferenceData()
    {
        Code = null!;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Code { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
}
