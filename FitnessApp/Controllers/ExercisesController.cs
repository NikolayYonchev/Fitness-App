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
using FitnessApp.Shared;
using FitnessApp.Models.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<IEnumerable<Exercise>>>> GetExercises()
        {
            var result = await _exerciseService.GetExercises();

            if (result.ErrorMessage == ErrorMessage.ExerciseNotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<ExerciseDto>>> GetExercise(int id)
        {
            var result = await _exerciseService.GetExercise(id);

            if (result.ErrorMessage == ErrorMessage.ExerciseNotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, Exercise exercise)
        {
            var result = await _exerciseService.PutExercise(id, exercise);

            if (!result.Success)
            {
                return result.ErrorMessage switch
                {
                    ErrorMessage.ExerciseNotFound => BadRequest(result.ErrorMessage),
                    ErrorMessage.ExerciseDoesNotExist => NotFound(result.ErrorMessage),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage)
                };
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(ExerciseDto exerciseDto)
        {
            var result = await _exerciseService.PostExercise(exerciseDto);

            return CreatedAtAction(nameof(GetExercise), new { id = result.Data.ExerciseId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var result = await _exerciseService.DeleteExercise(id);

            if (result.ErrorMessage == ErrorMessage.ExerciseNotFound)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }

    }
}
