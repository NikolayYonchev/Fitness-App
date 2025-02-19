using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<MealLog> MealLogs { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        //edin user trq da ima meallogs za vseki otdelen den

    }
}