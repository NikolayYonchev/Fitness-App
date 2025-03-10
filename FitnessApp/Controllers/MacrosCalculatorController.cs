using FitnessApp.Models;
using FitnessApp.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("api/macrosCalculator")]
    public class MacrosCalculatorController : Controller
    {
        [HttpPost]
        public IActionResult CalculateMacros(int age, double height, double weight,
            Gender gender, Activity activity, ExerciseGoal goal)
        {
            var result = new MacrosCalculator()
            {
                Activity = activity,
                ExerciseGoal = goal,
                Age = age,
                Gender = gender,
                Height = height,
                Weight = weight
            };
            
            return Ok(result.Calculate());
        }
    }
}
