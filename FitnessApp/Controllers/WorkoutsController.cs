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

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
        {
            return await _context.Workouts.ToListAsync();
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var result = new WorkoutDto()
            {
                WorkoutDuration = workout.WorkoutDuration,
                BodyPart = workout.BodyPart,
                Description = workout.Description,
                Name = workout.Name
            };

            return result;
        }

        // PUT: api/Workouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int id, Workout workout)
        {
            if (id != workout.WorkoutId)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Workouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> PostWorkout(WorkoutDto workoutDto)
        {
            //TODO: Add Exercises to a Workout
            var workout = new Workout()
            {
                BodyPart = workoutDto.BodyPart,
                Description = workoutDto.Description,
                Name = workoutDto.Name,
                WorkoutDuration = workoutDto.WorkoutDuration,
                //Po umno e da se suzdade prazen workout
            };
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = workout.WorkoutId }, workout);
        }


        [HttpPost("AddExercises")]
        public async Task<IActionResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds)
        {
            var workout = await _context.Workouts
                .FirstOrDefaultAsync(x => x.WorkoutId == workoutId);

            if (workout == null)
            {
                return NotFound("Workout not found.");
            }

            // Get all exercises that match the provided IDs
            var exercises = await _context.Exercises
                .Where(x => exerciseIds.Contains(x.ExerciseId) && workout.BodyPart == x.BodyPart)
                .ToListAsync();

            if (exercises.Count == 0)
            {
                return BadRequest("No valid exercises found.");
            }

            // Find existing links to avoid duplicates
            var existingExerciseIds = await _context.ExerciseWorkouts
                .Where(ew => ew.WorkoutId == workoutId && exerciseIds.Contains(ew.ExerciseId))
                .Select(ew => ew.ExerciseId)
                .ToListAsync();

            var newExerciseWorkouts = exercises
                .Where(ex => !existingExerciseIds.Contains(ex.ExerciseId)) // Exclude existing ones
                .Select(ex => new ExerciseWorkout
                {
                    ExerciseId = ex.ExerciseId,
                    WorkoutId = workoutId
                })
                .ToList();

            if (newExerciseWorkouts.Count == 0)
            {
                return BadRequest("All selected exercises are already added to the workout.");
            }

            _context.ExerciseWorkouts.AddRange(newExerciseWorkouts);
            await _context.SaveChangesAsync();

            return Ok($"Added {newExerciseWorkouts.Count} exercises to the workout.");
        }



        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
