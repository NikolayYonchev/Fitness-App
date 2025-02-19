using FitnessApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        //public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Workout>()
                .HasMany(x => x.Exercises)
                .WithOne();

            builder.Entity<Meal>()
                .HasMany(x => x.Foods)
                .WithOne();

            builder.Entity<User>()
                .HasMany(x => Workouts)
                .WithOne();

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.MealLog)
                .WithMany(ml => ml.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealLogId);

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.Meal)
                .WithMany(m => m.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealId);
        }
    }
}
