using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Models.Enums;
using FitnessApp.Services.Contracts;

namespace FitnessApp.Services
{
    public class MacrosCalculatorService : IMacrosCalculatorService
    {
        public MacrosDto CalculateMacros(MacrosCalculator data)
        {
            double activityFactor;
            double BMR;
            if (data.Gender == Gender.Male)
            {
                BMR = 10 * data.Weight + 6.25 * data.Height - 5 * data.Age + 5;
            }
            else if (data.Gender == Gender.Female)
            {
                BMR = 10 * data.Weight + 6.25 * data.Height - 5 * data.Age - 161;
            }
            else
            {
                throw new ArgumentException("Gender not set");
            }
            switch (data.Activity)
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
                    throw new ArgumentException("Invalid activity level.");
            }
            double TDEE = activityFactor * BMR;
            double goalCalories;
            double proteinFactor;

            if (data.ExerciseGoal == ExerciseGoal.LoseWeight)
            {
                goalCalories = TDEE - 500;
                proteinFactor = 1.5;
            }
            else if (data.ExerciseGoal == ExerciseGoal.StayFit)
            {
                goalCalories = TDEE;
                proteinFactor = 1.8;
            }
            else
            {
                goalCalories = TDEE + 300;
                proteinFactor = 2;
            }

            double protein = proteinFactor * data.Weight;
            double fats = (goalCalories * 0.25) / 9;
            double carbs = goalCalories - (protein * 4 + fats * 9) / 4;

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

