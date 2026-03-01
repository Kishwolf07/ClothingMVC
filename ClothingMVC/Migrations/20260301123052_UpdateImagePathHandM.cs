using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagePathHandM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "HandM/H&M-Polo-Shirt-Striped-Ladies.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "H&M/H&M-Polo-Shirt-Striped-Ladies.jpg");
        }
    }
}
