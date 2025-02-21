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

        public DbSet<Product> Foods { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealLog> MealLogs { get; set; }
        public DbSet<MealLogMeal> MealLogMeals { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserWorkout>()
                .HasKey(p => new { p.WorkoutId, p.UserId });

            builder.Entity<MealLogMeal>()
                .HasKey(k => new { k.MealId, k.MealLogId });

            builder.Entity<Workout>()
                .HasMany(x => x.Exercises)
                .WithOne();

            builder.Entity<Meal>()
                .HasMany(x => x.Foods)
                .WithOne();

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.MealLog)
                .WithMany(ml => ml.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealLogId);

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.Meal)
                .WithMany(m => m.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealId);

            builder.Entity<UserWorkout>()
                .HasOne(u => u.User)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserWorkout>()
                .HasOne(w => w.Workout)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(wi => wi.WorkoutId);
        }
    }
}
