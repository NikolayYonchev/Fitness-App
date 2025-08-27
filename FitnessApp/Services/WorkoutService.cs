using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Services.Contracts;
using FitnessApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using FitnessApp.Models.Enums;

namespace FitnessApp.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceEmptyResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds)
        {
            var workout = await _context.Workouts
                .Include(w => w.BodyPartWorkouts)
                .FirstOrDefaultAsync(x => x.WorkoutId == workoutId);

            if (!_context.Workouts.Any(w => w.WorkoutId == workoutId))
            {
                return new ServiceEmptyResult { Success = false, ErrorMessage = ErrorMessage.BadRequest/*"Workout is null" */};
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
                    return new ServiceEmptyResult { Success = false, ErrorMessage = ErrorMessage.ExerciseDoesNotMatchBodyParts/*$"Exercise {exercise.ExerciseId} does not match the workout's body parts."*/ }; ;
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

            return new ServiceEmptyResult()
            {
                Success = true,
                ErrorMessage = ErrorMessage.Success //"Exercises Added Successfully to Workout!"
            };
        }

        public async Task<ServiceEmptyResult> DeleteWorkout(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout == null)
            {
                return new ServiceEmptyResult { Success = false, ErrorMessage = ErrorMessage.BadRequest/*$"Workout is null"*/ }; ;
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return new ServiceEmptyResult { Success = false, ErrorMessage = ErrorMessage.Success /*$"Workout Deleted Successfully!"*/ }; ;
        }

        public async Task<ServiceResult<WorkoutDto>> GetWorkout(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);

            if (workout == null)
            {
                return new ServiceResult<WorkoutDto>()
                {
                    ErrorMessage = ErrorMessage.WorkoutNotFound, //"Workout is null",
                    Success = false
                };
            }

            var workoutDto = new WorkoutDto()
            {
                WorkoutDuration = workout.WorkoutDuration,
                BodyPartWorkouts = workout.BodyPartWorkouts,
                Description = workout.Description,
                Name = workout.Name
            };

            return new ServiceResult<WorkoutDto>()
            {
                ErrorMessage = ErrorMessage.Success, //"Workout Returned Successfully",
                Data = workoutDto,
                Success = true
            };
        }

        public async Task<ServiceResult<IEnumerable<Workout>>> GetWorkouts()
        {
            var workouts = await _context.Workouts.ToListAsync();

            if (workouts == null)
            {
                return new ServiceResult<IEnumerable<Workout>>()
                {
                    ErrorMessage = ErrorMessage.WorkoutNotFound,
                    Data = workouts,
                    Success = false
                };
            }

            return new ServiceResult<IEnumerable<Workout>>()
            {
                ErrorMessage = ErrorMessage.Success,//"Workouts Returned Successfully",
                Data = workouts,
                Success = true
            };
        }

        public async Task<ServiceResult<WorkoutDto>> PostWorkout(WorkoutDto workoutDto)
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

            var newDto = new WorkoutDto()
            {
                BodyPartWorkouts = workoutDto.BodyPartWorkouts,
                Description = workoutDto.Description,
                Name = workoutDto.Name,
                WorkoutDuration = workoutDto.WorkoutDuration,
            };

            return new ServiceResult<WorkoutDto>()
            {
                Success = true,
                Data = newDto,
                ErrorMessage = ErrorMessage.Success//"Workout Created Successfully!"
            };
        }

        public async Task<ServiceEmptyResult> PutWorkout(int workoutId, Workout workout)
        {
            if (workoutId != workout.WorkoutId)
            {
                return new ServiceEmptyResult()
                {
                    Success = false,
                    ErrorMessage = ErrorMessage.WorkoutIdNotFound // "WorkoutId doesn't match workout"
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
                    return new ServiceEmptyResult()
                    {
                        Success = false,
                        ErrorMessage = ErrorMessage.WorkoutDoesNotExist //"Workout does not exist"
                    };
                }
                else
                {
                    //this isn’t a missing workout — it’s some other concurrency issue.
                    throw;
                }
            }

            return new ServiceEmptyResult()
            {
                Success = true,
                ErrorMessage = ErrorMessage.Success //"Workout Modified Successfully!"
            };
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
