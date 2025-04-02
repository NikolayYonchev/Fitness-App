using System.ComponentModel.DataAnnotations;
using FitnessApp.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Models
{
    public class Meal
    {
        public Meal()
        {
            Products = new HashSet<Product>();
            MealLogMeals = new HashSet<MealLogMeal>();
        }
        public int MealId { get; set; }
        [Required]
        public string Name { get; set; }
        /*[Required]
        public Food Ingredient { get; set; }*/
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<MealLogMeal> MealLogMeals { get; set; }
    }
}