using FitnessApp.Data;
using FitnessApp.Models.Dtos;
using FitnessApp.Models;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Shared;
using FitnessApp.Models.Enums;

namespace FitnessApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;

        public ExerciseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<Exercise>>> GetExercises()
        {
            var result = await _context.Exercises.ToListAsync();

            if (result == null)
            {
                return new ServiceResult<IEnumerable<Exercise>>()
                {
                    Data = result,
                    ErrorMessage = ErrorMessage.ExerciseNotFound,
                    Success = false
                };
            }

            return new ServiceResult<IEnumerable<Exercise>>()
            {
                Data = result,
                ErrorMessage = ErrorMessage.Success,
                Success = true
            };
        }

        public async Task<ServiceResult<ExerciseDto>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            var result = new ExerciseDto()
            {
                Name = exercise.Name,
                BodyPart = exercise.BodyPart,
                CaloriesBurnedPerMinute = exercise.CaloriesBurnedPerMinute,
                Complexity = exercise.Complexity,
                Description = exercise.Description
            };

            if (result == null)
            {
                return new ServiceResult<ExerciseDto>()
                {
                    Success = false,
                    ErrorMessage = ErrorMessage.ExerciseNotFound,
                    Data = result
                };
            }

            return new ServiceResult<ExerciseDto>()
            {
                Success = true,
                ErrorMessage = ErrorMessage.Success,
                Data = result
            };
        }

        public async Task<ServiceEmptyResult> PutExercise(int id, Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return new ServiceEmptyResult()
                {
                    Success = false,
                    ErrorMessage = ErrorMessage.ExerciseNotFound,
                };
            }

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
                {
                    return new ServiceEmptyResult()
                    {
                        Success = false,
                        ErrorMessage = ErrorMessage.ExerciseNotFound,
                    };
                }
                else
                {
                    //todo should i be throwing here
                    throw;
                }
            }

            return  new ServiceEmptyResult()
            {
                Success = true,
                ErrorMessage = ErrorMessage.Success,
            };
        }

        public async Task<ServiceResult<ExerciseDto>> PostExercise(ExerciseDto dto)
        {
            var exercise = new Exercise
            {
                Description = dto.Description,
                Complexity = dto.Complexity,
                CaloriesBurnedPerMinute = dto.CaloriesBurnedPerMinute,
                BodyPart = dto.BodyPart,
                Name = dto.Name
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            var exerciseDto = new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Description = exercise.Description,
                Complexity = exercise.Complexity,
                CaloriesBurnedPerMinute = exercise.CaloriesBurnedPerMinute,
                BodyPart = exercise.BodyPart,
                Name = exercise.Name
            };

            return new ServiceResult<ExerciseDto>()
            {
                Success = true,
                Data = exerciseDto,
                ErrorMessage = ErrorMessage.Success
            };
        }


        public async Task<ServiceEmptyResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return new ServiceEmptyResult()
                {
                    Success = false,
                    ErrorMessage = ErrorMessage.ExerciseNotFound
                };
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return new ServiceEmptyResult()
            {
                Success = true,
                ErrorMessage = ErrorMessage.Success
            }; ;
        }

        protected bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }

    }
}
