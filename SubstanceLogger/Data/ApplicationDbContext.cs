using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SubstanceLogger.Models;

namespace SubstanceLogger.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Substance> Substances { get; set; }
    public DbSet<SubstanceLog> SubstanceLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<SubstanceLog>()
            .HasOne(sl => sl.Substance)
            .WithMany(s => s.Logs)
            .HasForeignKey(sl => sl.SubstanceId);

        // Seed some initial data
        modelBuilder.Entity<Substance>().HasData(
            new Substance { Id = 1, Name = "Caffeine", Category = "Stimulant", Description = "A central nervous system stimulant found in coffee, tea, and many energy drinks." },
            new Substance { Id = 2, Name = "Vitamin C", Category = "Vitamin", Description = "An essential nutrient involved in the repair of tissue and the enzymatic production of certain neurotransmitters." },
            new Substance { Id = 3, Name = "Melatonin", Category = "Hormone", Description = "A hormone that regulates sleep-wake cycles." }
        );
    }
}
