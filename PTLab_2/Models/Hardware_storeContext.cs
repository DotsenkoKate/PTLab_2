using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PTLab_2.Models
{
    public partial class Hardware_storeContext : DbContext
    {
        public Hardware_storeContext()
        {
        }

        public Hardware_storeContext(DbContextOptions<Hardware_storeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Hardware_store;Username=postgres;Password=4815162342");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .HasColumnType("char")
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasColumnType("char")
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasColumnType("char")
                    .HasColumnName("password");

                entity.Property(e => e.Purchase).HasColumnName("purchase");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("char")
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int")
                    .HasColumnName("quantity");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int")
                    .HasColumnName("customer_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int")
                    .HasColumnName("product_id");

            });
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Discount)
                    .HasColumnType("int")
                    .HasColumnName("Discount");
                entity.Property(e => e.Quantity)
                    .HasColumnType("int")
                    .HasColumnName("quantity");
                entity.Property(e => e.CustomerId)
                    .HasColumnType("int")
                    .HasColumnName("customer_id");
                entity.Property(e => e.ProductName)
                    .HasColumnType("char")
                    .HasColumnName("product_name");
                entity.Property(e => e.Price).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
