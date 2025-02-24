using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Calories { get; set; }

        [Required]
        [Range(0, 500)]
        public int Fat { get; set; }

        [Required]
        [Range(0, 500)]
        public int Protein { get; set; }

        [Required]
        [Range(0, 500)]
        public int Carbohydrates { get; set; }

    }
}
