using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace FitnessApp.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int CaloriesBurnedPerMinute { get; set; }
        [Required]
        public Complexity Complexity { get; set; }
        [Required]
        public BodyPart BodyPart { get; set; }
    }
}
