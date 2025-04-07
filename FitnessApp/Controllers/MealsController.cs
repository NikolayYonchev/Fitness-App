using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Models.ViewModels;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealViewModel>>> GetMeals()
        {
            var meals = await _context.Meals
                .Select(x => new MealViewModel()
                {
                    Calories = x.Calories,
                    Carbs = x.Carbs,
                    Fats = x.Fats,
                    Name = x.Name,
                    Protein = x.Protein,
                    ProductNames = x.Products.Select(y => y.Name)
                })
                .ToListAsync();

            return Ok(meals);
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealViewModel>> GetMeal(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);

            if (meal == null)
            {
                return NotFound();
            }

            var mealDto = new MealViewModel()
            {
                Calories = meal.Calories,
                Fats = meal.Fats,
                Carbs = meal.Carbs,
                Protein = meal.Protein,
                Description = meal.Description,
                Name = meal.Name,
                ProductNames = meal.Products.Select(x => x.Name).ToList()
            };

            return mealDto;
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.MealId)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Meals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(MealDto mealDto)
        {
            if (mealDto == null)
            {
                return NotFound();
            }
            var products = _context.Products
            .Where(x => mealDto.ProductNames
            .Contains(x.Name))
            .ToList();

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

            var mealViewModel = new MealViewModel
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
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = mealViewModel.MealId }, mealViewModel);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.MealId == id);
        }
    }
}
