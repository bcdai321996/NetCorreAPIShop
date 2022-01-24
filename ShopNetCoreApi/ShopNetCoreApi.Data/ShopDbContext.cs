using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShopNetCoreApi.Models.Entities;
using Version = ShopNetCoreApi.Models.Entities.Version;

namespace ShopNetCoreApi.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(i => new {i.OrderId, i.ProductId});
            modelBuilder.Entity<Permission>().HasKey(i => new { i.ActionId, i.FunctionId });
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRoles");
            modelBuilder.Entity<Role>().ToTable("Roles");


        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Entities.Action> Actions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Product> Productions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Models.Entities.Action> Versions { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<JobHistory> JobHistorys { get; set; }
        public DbSet<Education> Educations { get; set; }




    }
}
