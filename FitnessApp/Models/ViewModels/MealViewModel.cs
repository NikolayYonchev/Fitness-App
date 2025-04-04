namespace FitnessApp.Models.ViewModels
{
    public class MealViewModel
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }
        public IEnumerable<string>? ProductNames { get; set; }
    }
}
