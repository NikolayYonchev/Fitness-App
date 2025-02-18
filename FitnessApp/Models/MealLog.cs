using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    //Users log meals and track calories.
    public class MealLog
    {
        public int MealLogId { get; set; }
        public int FoodId { get; set; }
        public int UserId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Date { get; set; }
        public Food Food { get; set; }
        public User User { get; set; }

        //one user should have many meallogs and they should reset daily

    }
}
