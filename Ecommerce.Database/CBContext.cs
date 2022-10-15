using Ecommerce.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Ecommerce.Database
{
    public class CBContext : IdentityDbContext<CBUser>, IDisposable
    {
        public CBContext() : base("EcommerceDB")
        {
            System.Data.Entity.Database.SetInitializer<CBContext>(new CBContextInitializer());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public static CBContext Create()
        {
            return new CBContext();
        }
    }
}
