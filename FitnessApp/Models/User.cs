using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            MealLogs = new HashSet<MealLog>();
            //UserWorkout = new HashSet<UserWorkout>();
        }
        [Required]
        public string Name { get; set; }

        public ICollection<MealLog> MealLogs { get; set; }
        public ICollection<UserWorkout> UserWorkouts { get; set; }
        //one user should have mealogs for each seperate day

    }
}