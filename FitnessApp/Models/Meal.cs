using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Meal
    {
        public Meal()
        {
            Foods = new HashSet<Product>();
        }
        public int MealId { get; set; }
        [Required]
        public string Name { get; set; }
        /*[Required]
        public Food Ingredient { get; set; }*/
        public IEnumerable<Product> Foods { get; set; }
        public IEnumerable<MealLogMeal> MealLogMeals { get; set; }
    }
}
