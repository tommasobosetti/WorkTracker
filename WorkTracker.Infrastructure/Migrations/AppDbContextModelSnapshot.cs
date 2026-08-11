using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WorkTracker.Infrastructure.Data;

#nullable disable

namespace WorkTracker.Infrastructure.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

        modelBuilder.Entity("WorkTracker.Domain.Models.WorkDay", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd()
             .HasColumnType("INTEGER");
            b.Property<string>("Date").IsRequired().HasColumnType("TEXT");
            b.Property<int>("Type").HasColumnType("INTEGER");
            b.Property<string>("Notes").HasColumnType("TEXT");
            b.HasKey("Id");
            b.HasIndex("Date").IsUnique();
            b.ToTable("WorkDays");
        });

        modelBuilder.Entity("WorkTracker.Domain.Models.Stamp", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd()
             .HasColumnType("INTEGER");
            b.Property<int>("WorkDayId").HasColumnType("INTEGER");
            b.Property<string>("Time").IsRequired().HasColumnType("TEXT");
            b.HasKey("Id");
            b.HasIndex("WorkDayId");
            b.ToTable("Stamps");
        });

        modelBuilder.Entity("WorkTracker.Domain.Models.LeaveSlot", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd()
             .HasColumnType("INTEGER");
            b.Property<int>("WorkDayId").HasColumnType("INTEGER");
            b.Property<string>("From").IsRequired().HasColumnType("TEXT");
            b.Property<string>("To").IsRequired().HasColumnType("TEXT");
            b.Property<string>("Reason").HasColumnType("TEXT");
            b.HasKey("Id");
            b.HasIndex("WorkDayId");
            b.ToTable("LeaveSlots");
        });

        modelBuilder.Entity("WorkTracker.Domain.Models.Stamp", b =>
        {
            b.HasOne("WorkTracker.Domain.Models.WorkDay", "WorkDay")
             .WithMany("Stamps")
             .HasForeignKey("WorkDayId")
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });

        modelBuilder.Entity("WorkTracker.Domain.Models.LeaveSlot", b =>
        {
            b.HasOne("WorkTracker.Domain.Models.WorkDay", "WorkDay")
             .WithMany("LeaveSlots")
             .HasForeignKey("WorkDayId")
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });
#pragma warning restore 612, 618
    }
}
