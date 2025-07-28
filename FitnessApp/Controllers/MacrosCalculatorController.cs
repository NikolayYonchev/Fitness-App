using FitnessApp.Models;
using FitnessApp.Models.Enums;
using FitnessApp.Services;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("api/macrosCalculator")]
    public class MacrosCalculatorController : Controller
    {
        private readonly IMacrosCalculatorService macrosCalculatorService;

        public MacrosCalculatorController(IMacrosCalculatorService macrosCalculatorService)
        {
            this.macrosCalculatorService = macrosCalculatorService;
        }

        [HttpPost]
        public IActionResult CalculateMacros(MacrosCalculator input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = macrosCalculatorService.CalculateMacros(input);

            return Ok(result);
        }

        
    }
}
