using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Shared;

namespace FitnessApp.Services.Contracts
{
    public interface IWorkoutService
    {
        public Task<IEnumerable<Workout>> GetWorkouts();
        public Task<WorkoutDto> GetWorkout(int workoutId);
        public Task<ServiceResult> PutWorkout(int id, Workout workout);
        public Task<WorkoutDto> PostWorkout(WorkoutDto workoutDto);
        public Task<ServiceResult> AddExercisesToWorkout(int workoutId, List<int> exerciseIds);
        public Task<ServiceResult> DeleteWorkout(int id);
    }
}
