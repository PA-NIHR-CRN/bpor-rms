using Microsoft.EntityFrameworkCore;

namespace Bpor.Rms.Infrastructure.Entities;

public class ParticipantDbContext(DbContextOptions<ParticipantDbContext> options) : DbContext(options)
{
    public DbSet<Participant> Participants { get; set; } = null!;
}
