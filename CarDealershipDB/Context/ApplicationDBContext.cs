using System;
using System.Collections.Generic;
using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Context;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<ServicePrice> ServicePrices { get; set; }

    public virtual DbSet<Tire> Tires { get; set; }

    public virtual DbSet<TireInventory> TireInventories { get; set; }

    public virtual DbSet<TireService> TireServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prices__3214EC075915C566");

            entity.Property(e => e.Price1)
                .HasColumnType("money")
                .HasColumnName("Price");
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceP__3214EC074399825F");

            entity.ToTable("ServicePrice");

            entity.Property(e => e.Cost).HasColumnType("money");
        });

        modelBuilder.Entity<Tire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tires__3214EC0784928B08");

            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Seasonality)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Price).WithMany(p => p.Tires)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tires__PriceId__403A8C7D");

            entity.HasOne(d => d.TireInventory).WithMany(p => p.Tires)
                .HasForeignKey(d => d.TireInventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tires__TireInven__412EB0B6");
        });

        modelBuilder.Entity<TireInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TireInve__3214EC076073F121");

            entity.ToTable("TireInventory");
        });

        modelBuilder.Entity<TireService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TireServ__3214EC07D54BE900");

            entity.Property(e => e.ServiceName).HasMaxLength(50);

            entity.HasOne(d => d.Cost).WithMany(p => p.TireServices)
                .HasForeignKey(d => d.CostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TireServi__CostI__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
