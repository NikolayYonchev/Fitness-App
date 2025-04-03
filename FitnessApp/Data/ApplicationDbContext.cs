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

        public DbSet<Product> Products { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealLog> MealLogs { get; set; }
        public DbSet<MealLogMeal> MealLogMeals { get; set; }
        public DbSet<ExerciseWorkout> ExerciseWorkouts { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<BodyPartExercise> BodyPartExercises { get; set; }
        public DbSet<BodyPartWorkout> BodyPartWorkouts { get; set; }

        public DbSet<BodyPart> BodyParts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserWorkout>()
                .HasKey(p => new { p.WorkoutId, p.UserId });

            builder.Entity<MealLogMeal>()
                .HasKey(k => new { k.MealId, k.MealLogId });

            builder.Entity<ExerciseWorkout>()
                .HasKey(k => new { k.ExerciseId, k.WorkoutId });
           /* builder.Entity<Workout>()
                .HasMany(x => x.Exercises)
                .WithOne();*/

            builder.Entity<Meal>()
                .HasMany(x => x.Products)
                .WithOne();

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.MealLog)
                .WithMany(ml => ml.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealLogId);

            builder.Entity<MealLogMeal>()
                .HasOne(mlm => mlm.Meal)
                .WithMany(m => m.MealLogMeals)
                .HasForeignKey(mlm => mlm.MealId);

            builder.Entity<ExerciseWorkout>()
                .HasOne(e => e.Exercise)
                .WithMany(w => w.ExerciseWorkouts)
                .HasForeignKey(ei => ei.ExerciseId);

            builder.Entity<ExerciseWorkout>()
                .HasOne(e => e.Workout)
                .WithMany(w => w.ExerciseWorkouts)
                .HasForeignKey(ei => ei.WorkoutId);

            builder.Entity<UserWorkout>()
                .HasOne(u => u.User)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserWorkout>()
                .HasOne(w => w.Workout)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(wi => wi.WorkoutId);

            builder.Entity<BodyPartExercise>()
                .HasOne(e=>e.Exercise)
                .WithMany(b=>b.BodyPart)
                .hasfor
        }
    }
}
