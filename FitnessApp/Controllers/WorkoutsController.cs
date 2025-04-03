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
                BodyPartWorkouts = workout.BodyPartWorkouts,
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
                BodyPartWorkouts = workoutDto.BodyPartWorkouts,
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
            // Retrieve the workout and its associated body parts
            var workout = await _context.Workouts
                .Include(w => w.BodyPartWorkouts) // Ensure related data is loaded
                .FirstOrDefaultAsync(x => x.WorkoutId == workoutId);

            if (workout == null)
            {
                return NotFound("Workout not found");
            }

            // Extract the body part IDs associated with the workout
            var workoutBodyPartIds = workout.BodyPartWorkouts
                .Select(wb => wb.BodyPartId)
                .ToList();

            // Retrieve the exercises based on the input exercise IDs
            var exercises = await _context.Exercises
                .Where(e => exerciseIds.Contains(e.ExerciseId))
                .Include(e => e.BodyPartExercises) // Ensure related data is loaded
                .ToListAsync();

            // Check if all input exercises have at least one matching body part with the workout
            foreach (var exercise in exercises)
            {
                bool matchesWorkout = exercise.BodyPartExercises
                    .Any(be => workoutBodyPartIds.Contains(be.BodyPartId));

                if (!matchesWorkout)
                {
                    return BadRequest($"Exercise {exercise.ExerciseId} does not match the workout's body parts.");
                }
            }

            // If all exercises match, proceed with adding them to the workout
            foreach (var exercise in exercises)
            {
                _context.ExerciseWorkouts.Add(new ExerciseWorkout
                {
                    WorkoutId = workoutId,
                    ExerciseId = exercise.ExerciseId
                });
            }

            await _context.SaveChangesAsync();

            return Ok("Exercises successfully added to workout");
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
