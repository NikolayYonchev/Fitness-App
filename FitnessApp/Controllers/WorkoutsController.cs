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
using FitnessApp.Services;
using FitnessApp.Services.Contracts;
using FitnessApp.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using FitnessApp.Models.Enums;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService service)
        {
            _workoutService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
        {
            var result = await _workoutService.GetWorkouts();

            if (result.ResponseMessage == ResponseMessage.WorkoutNotFound)
            {
                return NotFound(result.ResponseMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<WorkoutDto>>> GetWorkout(int workoutId)
        {
            var result = await _workoutService.GetWorkout(workoutId);

            if (result.ResponseMessage == ResponseMessage.WorkoutNotFound)
            {
                return NotFound(result.ResponseMessage);
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int workoutId, Workout workout)
        {
            var result = await _workoutService.PutWorkout(workoutId, workout);

            if (!result.Success)
            {
                return result.ResponseMessage switch
                {
                    ResponseMessage.WorkoutIdNotFound => BadRequest(result.ResponseMessage),
                    ResponseMessage.WorkoutDoesNotExist => NotFound(result.ResponseMessage),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, result.ResponseMessage)
                };
            }

            return NoContent();

            //if (id != workout.WorkoutId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(workout).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!WorkoutExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<WorkoutDto>>> PostWorkout(WorkoutDto workoutDto)
        {
            var result = await _workoutService.PostWorkout(workoutDto);

            return CreatedAtAction(nameof(GetWorkout), new { id = result.Data.WorkoutId }, result.Data);

            //var workout = new Workout()
            //{
            //    BodyPartWorkouts = workoutDto.BodyPartWorkouts,
            //    Description = workoutDto.Description,
            //    Name = workoutDto.Name,
            //    WorkoutDuration = workoutDto.WorkoutDuration,
            //};
            //_context.Workouts.Add(workout);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetWorkout", new { id = workout.WorkoutId }, workout);
        }


        [HttpPost("AddExercises")]
        public async Task<IActionResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds)
        {
            var result = await _workoutService.AddExercisesToWorkout(workoutId, exerciseIds);

            if (!result.Success)
            {
                if (result.ResponseMessage == ResponseMessage.BadRequest) 
                {
                    return BadRequest(result.ResponseMessage);
                }
                else if (result.ResponseMessage == ResponseMessage.ExerciseDoesNotMatchBodyParts)
                {
                    return BadRequest(result.ResponseMessage);
                }
            }

            return Ok(); 

            //var workout = await _context.Workouts
            //    .Include(w => w.BodyPartWorkouts)
            //    .FirstOrDefaultAsync(x => x.WorkoutId == workoutId);

            //if (workout == null)
            //{
            //    return NotFound("Workout not found");
            //}

            //var workoutBodyPartIds = workout.BodyPartWorkouts
            //    .Select(wb => wb.BodyPartId)
            //    .ToList();

            //var exercises = await _context.Exercises
            //    .Where(e => exerciseIds.Contains(e.ExerciseId))
            //    .Include(e => e.BodyPartExercises)
            //    .ToListAsync();

            //foreach (var exercise in exercises)
            //{
            //    bool matchesWorkout = exercise.BodyPartExercises
            //        .Any(be => workoutBodyPartIds.Contains(be.BodyPartId));

            //    if (!matchesWorkout)
            //    {
            //        return BadRequest($"Exercise {exercise.ExerciseId} does not match the workout's body parts.");
            //    }
            //}

            //foreach (var exercise in exercises)
            //{
            //    _context.ExerciseWorkouts.Add(new ExerciseWorkout
            //    {
            //        WorkoutId = workoutId,
            //        ExerciseId = exercise.ExerciseId
            //    });
            //}

            //await _context.SaveChangesAsync();

            //return Ok("Exercises successfully added to workout");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int workoutId)
        {
            var workout = await _workoutService.DeleteWorkout(workoutId);

            if (workout.ResponseMessage == ResponseMessage.WorkoutNotFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool WorkoutExists(int id)
        //{
        //    return _context.Workouts.Any(e => e.WorkoutId == id);
        //}
    }
}
