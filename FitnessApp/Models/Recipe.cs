namespace FitnessApp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public Food Food { get; set; }
        public IEnumerable<Food> Foods { get; set; }
    }
}
