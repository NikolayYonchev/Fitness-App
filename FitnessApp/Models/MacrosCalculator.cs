using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MacrosCalculator
    {
        public int UserId { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Activity Activity { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public ExerciseGoal ExerciseGoal { get; set; }
    }
}
