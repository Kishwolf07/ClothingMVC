using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string? Name { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public BrandType Brand { get; set; }

        [Required]
        public string? ImagePath { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}