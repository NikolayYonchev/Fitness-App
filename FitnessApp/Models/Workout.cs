using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Workout
    {
        public Workout()
        {
            Exercises = new HashSet<Exercise>();
        }
        public int WorkoutId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public BodyPart BodyPart { get; set; }
        [Required]
        public int WorkoutDuration { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
