using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<MainCategory> MainCategories { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<WishlistItem> WishlistItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Rating)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SubCategoryId);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.MainCategory)
            .WithMany()
            .HasForeignKey(p => p.MainCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MainCategory>()
                .HasMany(m => m.SubCategories)
                .WithMany(s => s.MainCategories)
                .UsingEntity<Dictionary<string, object>>(
                    "MainCategorySubCategory",
                    j => j
                        .HasOne<SubCategory>()
                        .WithMany()
                        .HasForeignKey("SubCategoryId"),
                    j => j
                        .HasOne<MainCategory>()
                        .WithMany()
                        .HasForeignKey("MainCategoryId")
                );


            modelBuilder.Entity<Address>()
              .HasOne(a => a.User)
              .WithMany()
             .HasForeignKey(a => a.UserId);


            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);


            modelBuilder.Entity<MainCategory>()
              .ToTable("MainCategory");

            modelBuilder.Entity<SubCategory>()
                .ToTable("SubCategory");

            modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.User)
            .WithOne()
           .HasForeignKey<Wishlist>(w => w.UserId);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistId);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.Product)
                .WithMany()
                .HasForeignKey(wi => wi.ProductId);

            modelBuilder.Entity<Order>()
           .HasOne(o => o.User)
           .WithMany()
           .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Payment>()
            .HasOne<Order>()
            .WithOne()
            .HasForeignKey<Payment>(p => p.OrderId);


            modelBuilder.Entity<Payment>()
           .Property(p => p.Amount)
           .HasPrecision(18, 2);
        }
    }
}
