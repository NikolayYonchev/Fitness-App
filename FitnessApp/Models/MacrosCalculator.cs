using FitnessApp.Models.Dtos;
using FitnessApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MacrosCalculator
    {
        [Required]
        public double Height { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Activity Activity { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public ExerciseGoal ExerciseGoal { get; set; }

        public MacrosDto Calculate()
        {
            double activityFactor;
            double BMR;
            if (Gender == Gender.Male)
            {
                BMR = 10 * Weight + 6.25 * Height - 5 * Age + 5;
            }
            else if (Gender == Gender.Female)
            {
                BMR = 10 * Weight + 6.25 * Height - 5 * Age - 161;
            }
            else
            {
                throw new NullReferenceException("Gender not set");
            }
            switch (Activity)
            {
                case Activity.Sedentary:
                    activityFactor = 1.2;
                    break;

                case Activity.LightlyActive:
                    activityFactor = 1.375;
                    break;

                case Activity.ModeratelyActive:
                    activityFactor = 1.55;
                    break;

                case Activity.VeryActive:
                    activityFactor = 1.725;
                    break;

                case Activity.ExtraActive:
                    activityFactor = 1.9;
                    break;

                default:
                    activityFactor = -1;
                    break;
            }
            double TDEE = activityFactor * BMR;
            double goalCalories;
            double proteinFactor;

            if (ExerciseGoal == ExerciseGoal.LoseWeight)
            {
                goalCalories = TDEE - 500;
                proteinFactor = 1.5;
            }
            else if (ExerciseGoal == ExerciseGoal.StayFit)
            {
                goalCalories = TDEE;
                proteinFactor = 1.8;
            }
            else
            {
                goalCalories = TDEE + 300;
                proteinFactor = 2;
            }

            double protein = proteinFactor * Weight;
            double fats = (goalCalories * 0.25) / 9;
            double carbs = goalCalories - (protein * 4 + fats * 9)/4;

            return new MacrosDto()
            {
                Calories = (int)Math.Ceiling(goalCalories),
                Protein = (int)Math.Ceiling(protein),
                Carbs = (int)Math.Ceiling(carbs),
                Fats = (int)Math.Ceiling(fats)
            };
        }
    }
}
