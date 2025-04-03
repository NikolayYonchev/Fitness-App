namespace FitnessApp.Models
{
    public class BodyPartWorkout
    {
        public int BodyPartId { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public BodyPart BodyPart { get; set; }
    }
}
