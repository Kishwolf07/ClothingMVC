using ClothingMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Nike DryFit Masculina",
                    Price = 49.99m,
                    Description = "Comfort DryFit Masculina Nike",
                    Brand = BrandType.Nike,
                    ImagePath = "NIKE/Nike-DryFit-Masculina-Unisex.jpg"
                },
               new Product
               {
                   Id = 2,
                   Name = "Polo Shirt",
                   Price = 89.99m,
                   Description = "Warm Polo shirt Striped for ladies",
                   Brand = BrandType.HAndM,
                   ImagePath = "HandM/H&M-Polo-Shirt-Striped-Ladies.jpg"
               },
                new Product
                {
                    Id = 3,
                    Name = "Puma Sweatshirt",
                    Price = 39.99m,
                    Description = "Smooth Sweatshirt Unisex",
                    Brand = BrandType.Puma,
                    ImagePath = "PUMA/Puma-Basic-Sweatshirt-Unisex.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Middle Gauge V Neck",
                    Price = 99.99m,
                    Description = "Stylish Uniqlo middle gauge V neck for ladies",
                    Brand = BrandType.Uniqlo,
                    ImagePath = "UNIQLO/UNIQLO-Middle-Gauge-V-Neck-Cardigan-Womens.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Champion Sweatshirt",
                    Price = 59.99m,
                    Description = "Classic Polo Sweatshirt for Men",
                    Brand = BrandType.Champion,
                    ImagePath = "CHAMPION/Champion-Polo-Sweatshirt-for-Mens.jpg"
                }
            );
        }
    }
}
