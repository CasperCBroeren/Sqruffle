﻿using Microsoft.EntityFrameworkCore;
using Sqruffle.Domain.Product;
using Sqruffle.Domain.Product.Aspects;

namespace Sqruffle.Data
{
    public interface ISqruffleDatabase
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class SqruffleDatabase(DbContextOptions<SqruffleDatabase> options) : DbContext(options), ISqruffleDatabase

    {
        public DbSet<Product> Products { get; set; }

        public DbSet<AProductFeature> ProductAspects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Aspects)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
             
            modelBuilder.Entity<AProductFeature>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<Expires>(entity =>
            {
                entity.ToTable("Product_Expires");
            });

            modelBuilder.Entity<OwnershipRegistration>(entity =>
            {
                entity.ToTable("Product_OwnershipRegistration");
            });

            modelBuilder.Entity<PeriodicYield>(entity =>
            {
                entity.ToTable("Product_PeriodicYield");
            });
        }

    }
}
