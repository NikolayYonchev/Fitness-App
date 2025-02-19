namespace FitnessApp.Models
{
    public class MealLogMeal
    {
        public int MealLogId { get; set; }
        public MealLog MealLog { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
