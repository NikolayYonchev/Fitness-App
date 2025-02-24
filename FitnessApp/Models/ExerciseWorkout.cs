using System.Reflection.Metadata.Ecma335;

namespace FitnessApp.Models
{
    public class ExerciseWorkout
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
