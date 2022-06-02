using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking_Databased
{
    internal class DemoDBContext : DbContext
    {
        public DbSet<Office>? Offices { get; set; }
        public DbSet<Products>? Products { get; set; }
        public DbSet<ProductType>? ProductTypes { get; set; }

        string connectionString = @"Data Source = S4D03; Initial Catalog = Demobase; Integrated Security = True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>().HasData(new Office { ID = 1, OfficePlace = "Spain", Currency = "EUR" });
            modelBuilder.Entity<Office>().HasData(new Office { ID = 2, OfficePlace = "Sweden", Currency = "SEK" });
            modelBuilder.Entity<Office>().HasData(new Office { ID = 3, OfficePlace = "United Kingdom", Currency = "GBP" });

            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 1, TypeName = "Laptops" });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 2, TypeName = "Mobiles" });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 3, TypeName = "Computers" });

            modelBuilder.Entity<Products>().HasData(new Products { ID = 1, OfficeID = 2, ProductTypeID = 2, Brand = "Acer", Model = "Liquid Z900", Date = "2010-05-29", UKPrice = 120.00f });
            modelBuilder.Entity<Products>().HasData(new Products { ID = 2, OfficeID = 3, ProductTypeID = 1, Brand = "Acer", Model = "Nitro 5", Date = "2020-12-18", UKPrice = 855.00f });
            modelBuilder.Entity<Products>().HasData(new Products { ID = 3, OfficeID = 1, ProductTypeID = 3, Brand = "Alienware", Model = "AURORA RYZEN R10", Date = DateTime.Now.ToString("yyyy-MM-dd"), UKPrice = 1839.00f });
            modelBuilder.Entity<Products>().HasData(new Products { ID = 4, OfficeID = 1, ProductTypeID = 3, Brand = "HP", Model = "Pavilion TP01", Date = "2017-1-1", UKPrice = 1100.00f });
        }
    }
}
