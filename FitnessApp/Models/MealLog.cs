using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    //Users log meals and track calories.
    public class MealLog
    {
        public int MealLogId { get; set; }
        public int FoodId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Date { get; set; }
        public Product Food { get; set; }

        public IEnumerable<MealLogMeal> MealLogMeals { get; set; }

        //one user should have many meallogs and they should reset daily

    }
}
