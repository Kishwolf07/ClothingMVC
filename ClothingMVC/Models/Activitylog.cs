using System.ComponentModel.DataAnnotations;

namespace ClothingMVC.Models
{
    public class Activitylog
    {
        [Key]
        public int Id { get; set; }
        public string AdminEmail { get; set; } // Who did it?
        public string Action { get; set; }     // Create, Edit, or Delete?
        public string EntityName { get; set; } // Product Name
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Details { get; set; }    // Extra info
    }
}
