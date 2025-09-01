using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Models.Enums;
using FitnessApp.Models.ViewModels;
using FitnessApp.Services.Contracts;
using FitnessApp.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessApp.Services
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserContextService _userContext;

        public MealService(ApplicationDbContext dbContext, UserContextService userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<ServiceResult<IEnumerable<MealDto>>> GetMeals()
        {
            var meals = await _dbContext.Meals
                .Select(x => new MealDto()
                {
                    Calories = x.Calories,
                    Carbs = x.Carbs,
                    Fats = x.Fats,
                    Name = x.Name,
                    Protein = x.Protein,
                    ProductNames = x.Products.Select(y => y.Name)
                })
                .ToListAsync();

            return new ServiceResult<IEnumerable<MealDto>>()
            {
                Data = meals,
                ResponseMessage = ResponseMessage.Success,
                Success = true
            };
        }

        public async Task<ServiceResult<MealDto>> GetMeal(int mealId)
        {
            var meal = await _dbContext.Meals.FindAsync(mealId);

            if (meal == null)
            {
                return new ServiceResult<MealDto>()
                {
                    Success = false,
                    ResponseMessage = ResponseMessage.MealNotFound
                };
            }

            var mealDto = new MealDto()
            {
                Calories = meal.Calories,
                Fats = meal.Fats,
                Carbs = meal.Carbs,
                Protein = meal.Protein,
                Description = meal.Description,
                Name = meal.Name,
                ProductNames = meal.Products.Select(x => x.Name).ToList()
            };

            return new ServiceResult<MealDto>()
            {
                Success = true,
                ResponseMessage = ResponseMessage.Success,
                Data = mealDto
            };
        }

        public async Task<ServiceEmptyResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.MealId)
            {
                return new ServiceEmptyResult()
                {
                    Success = false,
                    ResponseMessage = ResponseMessage.BadRequest
                };
            }

            _dbContext.Entry(meal).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

           return new ServiceEmptyResult()
            {
                Success = true,
                ResponseMessage = ResponseMessage.Success
            };
            //return NoContent();
        }

        public async Task<ServiceResult<MealDto>> PostMeal(MealDto mealDto)
        {
            if (mealDto == null)

                return new ServiceResult<MealDto>()
                {
                    Success = false,
                    ResponseMessage = ResponseMessage.BadRequest
                };

            var products = await _dbContext.Products
                .Where(x => mealDto.ProductNames
                .Contains(x.Name))
                .ToListAsync();

            int calories = 0;
            int protein = 0;
            int fats = 0;
            int carbs = 0;

            foreach (var product in products)
            {
                calories = product.Calories;
                protein = product.Protein;
                fats = product.Fats;
                carbs = product.Carbohydrates;
            }
            var meal = new Meal()
            {
                Name = mealDto.Name,
                Products = products,
                Protein = protein,
                Calories = calories,
                Carbs = carbs,
                Fats = fats,
                Description = mealDto.Description
            };

            var newMealDto = new MealDto
            {
                MealId = meal.MealId,
                Name = meal.Name,
                Description = meal.Description,
                Protein = meal.Protein,
                Carbs = meal.Carbs,
                Fats = meal.Fats,
                Calories = meal.Calories,
                ProductNames = products.Select(p => p.Name).ToList()
            };
            _dbContext.Meals.Add(meal);
            await _dbContext.SaveChangesAsync();

            return new ServiceResult<MealDto>()
            {
                Data = newMealDto,
                ResponseMessage = ResponseMessage.Success,
                Success = true
            };
            //return CreatedAtAction("GetMeal", new
            //{
            //    id = mealViewModel.MealId
            //}, mealViewModel);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPost("LogMeal")]
        public async Task<ServiceResult<MealLogDto>> LogMeal(int mealId)
        {
            var userId = _userContext.GetUserId();

            var currentMeal = await _dbContext.Meals
                .FindAsync(mealId);

            var mealLog = new MealLog()
            {
                Date = DateTime.UtcNow.ToString(),
                UserId = userId,
            };

            _dbContext.MealLogs.Add(mealLog);
            await _dbContext.SaveChangesAsync();

            var mealLogMeal = new MealLogMeal()
            {
                MealId = mealId,
                MealLogId = mealLog.MealLogId
            };

            /*   var meal = new Meal()
               {
                   Calories = currentMeal.Calories,
                   Carbs = currentMeal.Carbs,
                   Description = currentMeal.Description,
                   Fats = currentMeal.Fats,
                   Name = currentMeal.Name,
                   Protein = currentMeal.Protein,
                   Products = currentMeal.Products
               };*/

            _dbContext.MealLogMeals.Add(mealLogMeal);
            await _dbContext.SaveChangesAsync();

            var mealLogDto = new MealLogDto()
            {
                Date = mealLog.Date,
                MealId = currentMeal.MealId,
                MealLogId = mealLog.MealLogId,
                UserId = userId
            };

            return new ServiceResult<MealLogDto>()
            {
                Data = mealLogDto,
                ResponseMessage = ResponseMessage.Success,
                Success = true
            };
            //return CreatedAtAction(nameof(LogMeal), new { id = mealLog.MealLogId }, mealLogDto);
        }

        public async Task<ServiceEmptyResult> DeleteMeal(int id)
        {
            var meal = await _dbContext.Meals.FindAsync(id);
            if (meal == null)
            {
                return new ServiceEmptyResult()
                {
                    Success = false,
                    ResponseMessage = ResponseMessage.MealNotFound
                };
            }

            _dbContext.Meals.Remove(meal);
            await _dbContext.SaveChangesAsync();

            return new ServiceEmptyResult()
            {
                ResponseMessage = ResponseMessage.Success,
                Success = true
            };
            //return NoContent();
        }
    }
}
