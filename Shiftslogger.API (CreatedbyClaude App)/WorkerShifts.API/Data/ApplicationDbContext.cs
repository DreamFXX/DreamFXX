using Microsoft.EntityFrameworkCore;
using WorkerShifts.API.Models;

namespace WorkerShifts.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Worker configuration
        modelBuilder.Entity<Worker>()
            .HasIndex(w => w.Id)
            .IsUnique();

        modelBuilder.Entity<Worker>()
            .Property(w => w.WorkerName)
            .HasMaxLength(50)
            .IsRequired();

        // Shift configuration
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.Shifts)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Shift>()
            .Property(s => s.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<Shift>()
            .Property(s => s.HourlyRate)
            .HasPrecision(10, 2);

        // Ignoring computed properties
        modelBuilder.Entity<Shift>()
            .Ignore(s => s.Duration);
    }
}
