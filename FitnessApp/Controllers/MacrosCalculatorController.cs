using FitnessApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    public class MacrosCalculatorController : Controller
    {
        public IActionResult CalculateMacros(int age, double height,double weight,Gender gender,/*activty and goal*/)
        {
            return View();
        }


    }
}
