using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Models.ViewModels;
using FitnessApp.Shared;

namespace FitnessApp.Services.Contracts
{
    public interface IMealService
    {
        public Task<ServiceResult<IEnumerable<MealDto>>> GetMeals();
        public Task<ServiceResult<MealDto>> GetMeal(int mealId);
        public Task<ServiceEmptyResult> PutMeal(int mealId, Meal meal);
        public Task<ServiceResult<MealDto>> PostMeal(MealDto mealDto);
        public Task<ServiceResult<MealLogDto>> LogMeal(int mealId);
        public Task<ServiceEmptyResult> DeleteMeal(int id);
    }
}
