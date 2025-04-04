using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models.Dtos
{
    public class WorkoutDto
    {
        public WorkoutDto()
        {
            BodyPartWorkouts = new HashSet<BodyPartWorkout>();
        }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        //public int BodyPartId { get; set; }
        //public BodyPart BodyPart { get; set; }
        [Required]
        public int WorkoutDuration { get; set; }

        public IEnumerable<BodyPartWorkout> BodyPartWorkouts { get; set; }
    }
}
