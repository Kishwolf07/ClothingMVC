using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the product name")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100,000")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please provide a description")]
        [StringLength(1000, ErrorMessage = "Description is too long")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a brand")]
        public BrandType Brand { get; set; }

        [Required(ErrorMessage = "Image path is required")]
        [Display(Name = "Image File Path")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 5000, ErrorMessage = "Quantity must be between 0 and 5,000")]
        public int Quantity { get; set; }
    }
}