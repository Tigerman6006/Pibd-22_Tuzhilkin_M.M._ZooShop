using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using ZooShop.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ZooShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        public DbSet<ProductBatch> ProductBatches { get; set; }
        public DbSet<StockNorm> StockNorms { get; set; }

 
        public DbSet<ReplenishmentRequest> ReplenishmentRequests { get; set; }


        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Salable>(entity =>
            {
                entity.UseTptMappingStrategy();

                entity.ToTable("Salables");

                entity.HasKey(s => s.SalableCode);

                entity.Property(s => s.SalableCode)
                    .ValueGeneratedOnAdd();

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(s => s.PurchasePrice)
                    .HasPrecision(12, 2);

                entity.Property(s => s.SellingPrice)
                    .HasPrecision(12, 2);
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.Property(p => p.ExpirationDate)
                    .IsRequired();

                entity.OwnsOne(p => p.StockNorm, norm =>
                {
                    norm.Property(n => n.MinimumStockLevel)
                        .HasColumnName("MinimumStockLevel");

                    norm.Property(n => n.MaximumStockLevel)
                        .HasColumnName("MaximumStockLevel");

                    norm.Property(n => n.ReviewPeriod)
                        .HasColumnName("ReviewPeriod");
                });

                entity.OwnsOne(p => p.StorageConditions, storage =>
                {
                    storage.Property(s => s.Temperature)
                        .HasColumnName("StorageTemperature");

                    storage.Property(s => s.Humidity)
                        .HasColumnName("StorageHumidity");

                    storage.Property(s => s.SpecialRequirements)
                        .HasColumnName("SpecialRequirements");
                });
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("Animals");

                entity.Property(a => a.Species)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Breed)
                    .HasMaxLength(100);
            });



            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategories");

                entity.HasKey(c => c.CategoryCode);

                entity.Property(c => c.CategoryCode)
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryCode)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<ProductBatch>(entity =>
            {
                entity.ToTable("ProductBatches");

                entity.HasKey(b => b.BatchNumber);

                entity.Property(b => b.BatchNumber)
                    .ValueGeneratedOnAdd();

                entity.Property(b => b.QuantityReceived)
                    .IsRequired();

                entity.Property(b => b.RemainingQuantity)
                    .IsRequired();

                entity.HasOne(b => b.Products)
                    .WithMany()
                    .HasForeignKey("ProductCode")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Supplier)
                    .WithMany()
                    .HasForeignKey("SupplierCode")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers");

                entity.HasKey(s => s.SupplierCode);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(s => s.PhoneNumber)
                    .HasMaxLength(30);

                entity.Property(s => s.DeliveryLeadTime)
                    .IsRequired();
                modelBuilder.Entity<Supplier>()
                    .HasMany(s => s.ReplenishmentRequests)
                    .WithOne(r => r.Supplier)
                    .HasForeignKey("SupplierCode")
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });



            modelBuilder.Entity<ReplenishmentRequest>(entity =>
            {
                entity.ToTable("ReplenishmentRequests");

                entity.HasKey(r => r.RequestNumber);

                entity.Property(r => r.Status)
                    .HasConversion<string>()
                    .HasMaxLength(30);

                entity.HasOne(r => r.Product)
                    .WithMany()
                    .HasForeignKey("ProductCode")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasKey(s => s.ReceiptNumber);

                entity.Property(s => s.Amount)
                    .HasPrecision(12, 2);

                entity.Property(s => s.PaymentMethod)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.ToTable("SaleItems");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.Price)
                    .HasPrecision(12, 2);

                entity.Property(i => i.Discount)
                    .HasPrecision(5, 2);

                entity.Property(i => i.Quantity)
                    .IsRequired();

                entity.HasOne(i => i.Salable)
                    .WithMany()
                    .HasForeignKey(i => i.SalableCode)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Sale)
                    .WithMany(s => s.SaleItems)
                    .HasForeignKey(i => i.ReceiptNumber)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
