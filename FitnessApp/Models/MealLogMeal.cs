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
// edin meallog trq da ima kolekciq ot mealove
//ivancho si logva v meallog s negovo id che e izql zakuska
// zakuskata mu e mealid

// zakuska s user ivancho
// v meallogmeals ima kolekciq to meals