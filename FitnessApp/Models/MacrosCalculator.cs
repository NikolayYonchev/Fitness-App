using FitnessApp.Models.Enums;

namespace FitnessApp.Models
{
    public class MacrosCalculator
    {
        public int UserId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public Activity Activity { get; set; }
        public Gender Gender { get; set; }
        public ExerciseGoal ExerciseGoal { get; set; }
    }
}
