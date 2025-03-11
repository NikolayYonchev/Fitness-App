using FitnessApp;
using FitnessApp.Controllers;
using FitnessApp.Data;
using FitnessApp.Middleware;
using FitnessApp.Models;
using FitnessApp.Models.Enums;
using FitnessApp.Services;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],  // ?? Get from appsettings.json
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//builder.Services.AddTransient<IUserService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services
    .AddControllers() // or AddControllers() in a Web API
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

//dark mode
/*app.UseStaticFiles();
app.UseSwaggerUI(options =>
{
    options.InjectJavascript("swagger-ui-theme.js");
});*/


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope()) // Create a scope to access services
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Ensure the database is created (for in-memory or new databases)
    dbContext.Database.EnsureCreated();

    // Check if exercises already exist to prevent duplicates
    if (!dbContext.Exercises.Any())
    {
        dbContext.Exercises.AddRange(new List<Exercise>
        {
            new Exercise { Name = "Push-Up",
                Description = "A basic upper-body exercise",
                CaloriesBurnedPerMinute = 8,
                BodyPart = BodyPart.Chest,
                Complexity = Complexity.Low
            },
            new Exercise {
                Name = "Squat",
                Description = "A lower-body strength exercise",
                CaloriesBurnedPerMinute = 7,
                 BodyPart = BodyPart.Legs,
                Complexity = Complexity.Medium
            },
            new Exercise { Name = "Jumping Jacks",
                Description = "A full-body aerobic exercise",
                CaloriesBurnedPerMinute = 10,
                 BodyPart = BodyPart.Chest,
                Complexity = Complexity.Low
            },
            new Exercise {
                  Name = "Pull-Up",
                  Description = "An upper-body exercise that strengthens the back and arms",
                  CaloriesBurnedPerMinute = 10,
                  BodyPart = BodyPart.Back,
                  Complexity = Complexity.High
            },
            new Exercise {
                Name = "Plank",
                Description = "A core-strengthening isometric exercise",
                CaloriesBurnedPerMinute = 5,
                BodyPart = BodyPart.Abs,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Lunges",
                Description = "A lower-body exercise targeting legs and glutes",
                CaloriesBurnedPerMinute = 8,
                BodyPart = BodyPart.Legs,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Burpees",
                Description = "A high-intensity full-body exercise combining strength and cardio",
                CaloriesBurnedPerMinute = 15,
                BodyPart = BodyPart.Abs,
                Complexity = Complexity.High
            },
            new Exercise {
                Name = "Bench Press",
                Description = "An upper-body strength exercise for chest development",
                CaloriesBurnedPerMinute = 9,
                BodyPart = BodyPart.Chest,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Bicycle Crunches",
                Description = "A dynamic core exercise targeting abdominal muscles",
                CaloriesBurnedPerMinute = 7,
                BodyPart = BodyPart.Abs,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Deadlift",
                Description = "A compound strength exercise for posterior chain development",
                CaloriesBurnedPerMinute = 11,
                BodyPart = BodyPart.Back,
                Complexity = Complexity.High
            },
            new Exercise {
                Name = "Mountain Climbers",
                Description = "A dynamic exercise that builds core strength and cardiovascular endurance",
                CaloriesBurnedPerMinute = 12,
                BodyPart = BodyPart.Abs,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Lateral Raises",
                Description = "An isolation exercise targeting the shoulder muscles",
                CaloriesBurnedPerMinute = 6,
                BodyPart = BodyPart.Shoulders,
                Complexity = Complexity.Low
            },
            new Exercise {
                Name = "Russian Twists",
                Description = "A rotational core exercise targeting the obliques",
                CaloriesBurnedPerMinute = 7,
                BodyPart = BodyPart.Abs,
                Complexity = Complexity.Medium
            },
            new Exercise {
                Name = "Bicep Curls",
                Description = "A traditional bicep exercise",
                CaloriesBurnedPerMinute = 7,
                BodyPart = BodyPart.Arms,
                Complexity = Complexity.Medium
            }
        });

        dbContext.SaveChanges(); // Save to database
        Console.WriteLine("Exercises seeded successfully!");
    }
    else
    {
        Console.WriteLine("Exercises already exist. Skipping seeding.");
    }
}


app.UseMiddleware<ErrorHandlingMiddleware>(); // Register error handling middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
