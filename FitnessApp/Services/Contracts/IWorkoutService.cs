using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Shared;

namespace FitnessApp.Services.Contracts
{
    public interface IWorkoutService
    {
        public Task<ServiceResult<IEnumerable<Workout>>> GetWorkouts();
        public Task<ServiceResult<WorkoutDto>> GetWorkout(int workoutId);
        public Task<ServiceEmptyResult> PutWorkout(int id, Workout workout);
        public Task<ServiceResult<WorkoutDto>> PostWorkout(WorkoutDto workoutDto);
        public Task<ServiceEmptyResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds);
        public Task<ServiceEmptyResult> DeleteWorkout(int id);
    }
}
