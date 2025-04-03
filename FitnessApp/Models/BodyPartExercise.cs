using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Models
{
    public class BodyPartExercise
    {
        public int BodyPartId { get; set; }
        public int ExerciseId { get; set; }

        public BodyPart BodyPart { get; set; }
        public Exercise Exercise { get; set; }
    }
}
