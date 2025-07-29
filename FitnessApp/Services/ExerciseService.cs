using FitnessApp.Data;
using FitnessApp.Models.Dtos;
using FitnessApp.Models;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;

        public ExerciseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<ExerciseDto> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return null;
            }

            var result = new ExerciseDto()
            {
                Name = exercise.Name,
                BodyPart = exercise.BodyPart,
                CaloriesBurnedPerMinute = exercise.CaloriesBurnedPerMinute,
                Complexity = exercise.Complexity,
                Description = exercise.Description
            };
            return result;
        }

        public async Task<bool> PutExercise(int id, Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<ExerciseDto> PostExercise(ExerciseDto dto)
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

            return new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Description = exercise.Description,
                Complexity = exercise.Complexity,
                CaloriesBurnedPerMinute = exercise.CaloriesBurnedPerMinute,
                BodyPart = exercise.BodyPart,
                Name = exercise.Name
            };
        }


        public async Task<bool> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return false;
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return true;
        }

        protected bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }

    }
}
