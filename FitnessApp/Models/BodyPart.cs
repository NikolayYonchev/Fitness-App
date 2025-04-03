using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class BodyPart
    {
        public int BodyPartId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
