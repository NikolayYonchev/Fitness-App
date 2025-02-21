using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FitnessApp.Models
{
    public class Workout
    {
        public Workout()
        {
            Exercises = new HashSet<Exercise>();
            //UserWorkouts = new HashSet<UserWorkout>();
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
        public IEnumerable<UserWorkout> UserWorkouts { get; set; }
    }
}
