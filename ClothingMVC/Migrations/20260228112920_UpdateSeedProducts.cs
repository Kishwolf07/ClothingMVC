using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "NIKE/Nike-DryFit-Masculina-Unisex.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "HandM/H&M-Polo-Shirt-Striped-Ladies.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "PUMA/Puma-Basic-Sweatshirt-Unisex.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "UNIQLO/UNIQLO-Middle-Gauge-V-Neck-Cardigan-Womens.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "CHAMPION/Champion-Polo-Sweatshirt-for-Mens.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "NIKE/Nike-DryFit-Masculina-Unisex");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "H&M/H&M-Polo-Shirt-Striped-Ladies");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "PUMA/Puma-Basic-Sweatshirt-Unisex");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "UNIQLO/UNIQLO-Middle-Gauge-V-Neck-Cardigan-Womens");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "CHAMPION/Champion-Polo-Sweatshirt-for-Mens");
        }
    }
}
