using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Meal
    {
        public Meal()
        {
            Foods = new HashSet<Food>();
        }
        public int MealId { get; set; }
        [Required]
        public string Name { get; set; }
        /*[Required]
        public Food Ingredient { get; set; }*/
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<MealLogMeal> MealLogMeals { get; set; }
    }
}
