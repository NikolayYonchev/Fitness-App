namespace FitnessApp.Models
{
    public class MealLog
    {
        public int MealLogId { get; set; }
        public int FoodId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }
        public Food Food { get; set; }
        public User User { get; set; }

    }
}
