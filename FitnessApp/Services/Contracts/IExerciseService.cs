using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Services.Contracts
{
    public interface IExerciseService
    {
        Task<ServiceResult<IEnumerable<Exercise>>> GetExercises();
        Task<ServiceResult<ExerciseDto>> GetExercise(int id);
        Task<ServiceEmptyResult> PutExercise(int id, Exercise exercise);
        Task<ServiceResult<ExerciseDto>> PostExercise(ExerciseDto dto);
        Task<ServiceEmptyResult> DeleteExercise(int id);
        //bool ExerciseExists(int id);
    }
}
