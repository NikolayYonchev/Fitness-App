using FitnessApp.Models.Dtos;

namespace FitnessApp.Services.Contracts
{
    public interface IUserService
    {
        UserProfileDto GetUserProfile(string userName);
    }
}
