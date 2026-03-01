using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClothingMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Comfort DryFit Masculina Nike", "NIKE/Nike-DryFit-Masculina-Unisex", "Nike DryFit Masculina", 49.99m },
                    { 2, 1, "Warm Polo shirt Striped for ladies", "H&M/H&M-Polo-Shirt-Striped-Ladies", "Polo Shirt", 89.99m },
                    { 3, 2, "Smooth Sweatshirt Unisex", "PUMA/Puma-Basic-Sweatshirt-Unisex", "Puma Sweatshirt", 39.99m },
                    { 4, 3, "Stylish Uniqlo middle gauge V neck for ladies", "UNIQLO/UNIQLO-Middle-Gauge-V-Neck-Cardigan-Womens", "Middle Gauge V Neck", 99.99m },
                    { 5, 4, "Classic Polo Sweatshirt for Men", "CHAMPION/Champion-Polo-Sweatshirt-for-Mens", "Champion Sweatshirt", 59.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
