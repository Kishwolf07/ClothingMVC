using System.ComponentModel.DataAnnotations;

namespace ClothingMVC.Models
{
    public enum BrandType
    {
        Nike = 0,
        [Display(Name = "H&M")]
        HAndM = 1,
        Puma = 2,
        Uniqlo  = 3,
        Champion = 4
    }   
}
