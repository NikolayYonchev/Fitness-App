﻿using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models.Dtos
{
    public class ExerciseDto
    {
        //public int ExerciseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int CaloriesBurnedPerMinute { get; set; }
        public string? Description { get; set; }
        [Required]
        public Complexity Complexity { get; set; }
        [Required]
        public BodyPart BodyPart { get; set; }

    }
}
