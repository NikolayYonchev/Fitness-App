using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Services.Contracts;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
        {
            var exercises = await exerciseService.GetExercises();

            if (exercises == null) return NotFound();   

            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetExercise(int id)
        {
            var exercise = await exerciseService.GetExercise(id);

            if (exercise == null) return NotFound();

            return Ok(exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wasUpdated = await exerciseService.PutExercise(id, exercise);

            if (!wasUpdated) return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(ExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wasPosted = await exerciseService.PostExercise(exerciseDto);

            return CreatedAtAction(nameof(GetExercise), new { id = wasPosted.ExerciseId }, wasPosted);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var wasDeleted = await exerciseService.DeleteExercise(id);

            if (!wasDeleted) return NotFound();

            return NoContent();
        }

    }
}
