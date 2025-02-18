using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}