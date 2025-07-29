using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Services.Contracts
{
    public interface IExerciseService
    {
        Task<IEnumerable<Exercise>> GetExercises();
        Task<ExerciseDto> GetExercise(int id);
        Task<bool> PutExercise(int id, Exercise exercise);
        Task<ExerciseDto> PostExercise(ExerciseDto dto);
        Task<bool> DeleteExercise(int id);
        //bool ExerciseExists(int id);
    }
}
