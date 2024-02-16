using System.ComponentModel.DataAnnotations;

namespace PetroPrice_MVC.Models
{
    public class PetrolStation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Station Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Price(¢/litre)")]
        public decimal Price { get; set; }
    }
}
