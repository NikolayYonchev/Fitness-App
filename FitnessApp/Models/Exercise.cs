﻿using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace FitnessApp.Models
{
    public class Exercise
    {
        public Exercise()
        {
            ExerciseWorkouts = new HashSet<ExerciseWorkout>();
            BodyPartWorkouts = new HashSet<BodyPartWorkout>();
        }
        public int ExerciseId { get; set; }
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

        public IEnumerable<BodyPartWorkout> BodyPartWorkouts { get; set; }
        public IEnumerable<ExerciseWorkout> ExerciseWorkouts { get; set; }
    }
}
