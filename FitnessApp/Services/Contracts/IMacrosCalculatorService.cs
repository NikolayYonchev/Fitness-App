using FitnessApp.Models;
using FitnessApp.Models.Dtos;

namespace FitnessApp.Services.Contracts
{
    public interface IMacrosCalculatorService
    {
        MacrosDto CalculateMacros(MacrosCalculator data);
    }
}
