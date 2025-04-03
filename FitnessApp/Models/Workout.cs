using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FitnessApp.Models
{
    public class Workout
    {
        public Workout()
        {
            ExerciseWorkouts = new HashSet<ExerciseWorkout>();
            UserWorkouts = new HashSet<UserWorkout>();
            BodyPartWorkouts = new HashSet<BodyPartWorkout>();
            BodyParts = new List<BodyPart>();
        }
        public int WorkoutId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public IEnumerable<BodyPart> BodyParts { get; set; }
        [Required]
        public int WorkoutDuration { get; set; }
        public IEnumerable<ExerciseWorkout> ExerciseWorkouts { get; set; }
        public IEnumerable<BodyPartWorkout> BodyPartWorkouts { get; set; }
        public IEnumerable<UserWorkout> UserWorkouts { get; set; }
    }
}
