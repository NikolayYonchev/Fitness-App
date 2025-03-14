using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            MealLogs = new HashSet<MealLog>();
            //Workouts = new HashSet<Workout>();
            UserWorkouts = new HashSet<UserWorkout>();
        }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<MealLog> MealLogs { get; set; }
        public ICollection<UserWorkout> UserWorkouts { get; set; }
        //public ICollection<Workout> Workouts { get; set; }

        //one user should have mealogs for each seperate day

    }
}