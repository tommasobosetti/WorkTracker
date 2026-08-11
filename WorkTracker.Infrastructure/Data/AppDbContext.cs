using Microsoft.EntityFrameworkCore;
using WorkTracker.Domain.Models;

namespace WorkTracker.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WorkDay> WorkDays => Set<WorkDay>();
    public DbSet<Stamp> Stamps => Set<Stamp>();
    public DbSet<LeaveSlot> LeaveSlots => Set<LeaveSlot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkDay>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Date).IsRequired();
            e.HasIndex(x => x.Date).IsUnique();
            e.Property(x => x.Type).IsRequired();
            e.HasMany(x => x.Stamps)
             .WithOne(x => x.WorkDay)
             .HasForeignKey(x => x.WorkDayId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.LeaveSlots)
             .WithOne(x => x.WorkDay)
             .HasForeignKey(x => x.WorkDayId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Stamp>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Time).IsRequired();
        });

        modelBuilder.Entity<LeaveSlot>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.From).IsRequired();
            e.Property(x => x.To).IsRequired();
        });
    }
}
