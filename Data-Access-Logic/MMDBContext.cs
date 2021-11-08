﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

#nullable disable

namespace Data_Access_Logic
{
    public partial class MMDBContext : DbContext
    {
        public MMDBContext()
        {
        }

        public MMDBContext(DbContextOptions<MMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LineItems> LineItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StoreFront> Storefronts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Email, "UQ__customer__AB6E61642D925A68")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<LineItems>(entity =>
            {
                entity.ToTable("line_item");

                entity.Property(e => e.LineItemsId).HasColumnName("line_item_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreFrontId).HasColumnName("storefront_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__line_item__produ__160F4887");

                entity.HasOne(d => d.StoreFront)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.StoreFrontId)
                    .HasConstraintName("FK__line_item__store__17036CC0");
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order_");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.StoreFrontId).HasColumnName("storefront_id");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order__FK");

                entity.HasOne(d => d.StoreFront)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.StoreFrontId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order___storefro__1332DBDC");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("brand");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(750)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<StoreFront>(entity =>
            {
                entity.ToTable("storefront");

                entity.Property(e => e.StoreFrontId).HasColumnName("storefront_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
