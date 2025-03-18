using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models.Dtos
{
    public class WorkoutDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public BodyPart BodyPart { get; set; }
        [Required]
        public int WorkoutDuration { get; set; }
        public IEnumerable<ExerciseWorkout> ExerciseWorkouts { get; set; }

    }
}
