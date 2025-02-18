using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Foods = new HashSet<Food>();
        }
        public int RecipeId { get; set; }
        [Required]
        public string Name { get; set; }
        /*[Required]
        public Food Ingredient { get; set; }*/
        public IEnumerable<Food> Foods { get; set; }
    }
}
