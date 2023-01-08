using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineShop
{
    internal class DbContextShop : DbContext
    {
        public DbContextShop() : base("name-DbContextShop")
        {
            this.Database.Connection.ConnectionString = @"Server=BUD;Database=OnlineShopDataBase;Trusted_Connection=True";
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<PurchaseHistoryItem> PurchaseHistoryItems { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Basket>().HasKey(b => b.Id);
            modelBuilder.Entity<PurchaseHistory>().HasKey(ph => ph.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Basket>()
            .HasRequired(b => b.Customer)
            .WithMany(c => c.Baskets)
            .HasForeignKey(b => b.CustomerId);


        }*/
    }
}
