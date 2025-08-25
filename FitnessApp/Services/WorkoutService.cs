using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Services.Contracts;
using FitnessApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FitnessApp.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds)
        {
            var workout = await _context.Workouts
                .Include(w => w.BodyPartWorkouts)
                .FirstOrDefaultAsync(x => x.WorkoutId == workoutId);

            if (workout == null)
            {
                return new ServiceResult { Success = false, ErrorMessage = "Workout is null" };
            }

            var workoutBodyPartIds = workout.BodyPartWorkouts
                .Select(wb => wb.BodyPartId)
                .ToList();

            var exercises = await _context.Exercises
                .Where(e => exerciseIds.Contains(e.ExerciseId))
                .Include(e => e.BodyPartExercises)
                .ToListAsync();

            foreach (var exercise in exercises)
            {
                bool matchesWorkout = exercise.BodyPartExercises
                    .Any(be => workoutBodyPartIds.Contains(be.BodyPartId));

                if (!matchesWorkout)
                {
                    //Is this the correct way of doing it
                    return new ServiceResult { Success = false, ErrorMessage = $"Exercise {exercise.ExerciseId} does not match the workout's body parts." }; ;
                }
            }

            foreach (var exercise in exercises)
            {
                _context.ExerciseWorkouts.Add(new ExerciseWorkout
                {
                    WorkoutId = workoutId,
                    ExerciseId = exercise.ExerciseId
                });
            }

            await _context.SaveChangesAsync();

            return new ServiceResult()
            {
                Success = true,
                ErrorMessage = "Success!"
            };
        }

        public async Task<ServiceResult> DeleteWorkout(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout == null)
            {
                return new ServiceResult { Success = false, ErrorMessage = $"Workout is null" }; ;
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return new ServiceResult { Success = false, ErrorMessage = $"Success!" }; ;
        }

        public async Task<WorkoutDto> GetWorkout(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);

            if (workout == null)
            {
                return null;
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

        public async Task<IEnumerable<Workout>> GetWorkouts()
        {
            return await _context.Workouts.ToListAsync();
        }

        public async Task<WorkoutDto> PostWorkout(WorkoutDto workoutDto)
        {
            var workout = new Workout()
            {
                BodyPartWorkouts = workoutDto.BodyPartWorkouts,
                Description = workoutDto.Description,
                Name = workoutDto.Name,
                WorkoutDuration = workoutDto.WorkoutDuration,
            };
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return new WorkoutDto()
            {
                BodyPartWorkouts = workoutDto.BodyPartWorkouts,
                Description = workoutDto.Description,
                Name = workoutDto.Name,
                WorkoutDuration = workoutDto.WorkoutDuration,
            };
        }

        public async Task<ServiceResult> PutWorkout(int workoutId, Workout workout)
        {
            if (workoutId != workout.WorkoutId)
            {
                return new ServiceResult()
                {
                    Success =false,
                    ErrorMessage = "WorkoutId doesn't match workout"
                };
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(workoutId))
                {
                    return new ServiceResult()
                    {
                        Success = false,
                        ErrorMessage = "Workout does not exist"
                    };
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult()
            {
                Success = true,
                ErrorMessage = "Success!"
            };
        }
        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
