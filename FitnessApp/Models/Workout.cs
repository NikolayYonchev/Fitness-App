using FitnessApp.Models.Enums;

namespace FitnessApp.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BodyPart BodyPart { get; set; }
        public int WorkoutDuration { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
