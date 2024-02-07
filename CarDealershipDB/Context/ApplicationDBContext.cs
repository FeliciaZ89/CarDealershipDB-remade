//using System;
//using System.Collections.Generic;
//using CarDealershipDB.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace CarDealershipDB.Context;

//public partial class ApplicationDBContext : DbContext
//{
//    public ApplicationDBContext()
//    {
//    }

//    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<PricesEntity> Prices { get; set; }

//    public virtual DbSet<ServicePriceEntity> ServicePrices { get; set; }

//    public virtual DbSet<TiresEntity> Tires { get; set; }

//    public virtual DbSet<TireInventoryEntity> TireInventories { get; set; }

//    public virtual DbSet<TireServicesEntity> TireServices { get; set; }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<PricesEntity>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Prices__3214EC07759F7F10");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//        });

//        modelBuilder.Entity<ServicePriceEntity>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__ServiceP__3214EC07DFB4A7F5");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//        });

//        modelBuilder.Entity<TiresEntity>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Tires__3214EC073205C978");

//            entity.HasOne(d => d.Price).WithMany(p => p.Tires)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Tires__PriceId__403A8C7D");

//            entity.HasOne(d => d.TireInventory).WithMany(p => p.Tires)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Tires__TireInven__412EB0B6");
//        });

//        modelBuilder.Entity<TireInventoryEntity>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__TireInve__3214EC07BA5B37B2");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//        });

//        modelBuilder.Entity<TireServicesEntity>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__TireServ__3214EC07BE7D57FF");

//            entity.Property(e => e.Id).ValueGeneratedNever();

//            entity.HasOne(d => d.Cost).WithMany(p => p.TireServices)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__TireServi__CostI__398D8EEE");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
