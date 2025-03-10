using FitnessApp.Models;
using FitnessApp.Models.Dtos;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Services
{
    public class UserService : IUserService
    {
        public UserProfileDto GetUserProfile(string userName)
        {
            return new UserProfileDto { Message = $"Hello, {userName}! This is your secure profile." };
        }
    }
}
