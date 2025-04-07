using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    //Users log meals and track calories.
    public class MealLog
    {
        public MealLog()
        {
            MealLogMeals = new HashSet<MealLogMeal>();
        }
        public int MealLogId { get; set; }
        public string UserId { get; set; }
        //public int MealId { get; set; }
        //public Meal Meal { get; set; }
        //[Required]
        //public int Quantity { get; set; }
        [Required]
        public string Date { get; set; }

        public IEnumerable<MealLogMeal> MealLogMeals { get; set; }

        //one user should have many meallogs and they should reset daily

    }
}
