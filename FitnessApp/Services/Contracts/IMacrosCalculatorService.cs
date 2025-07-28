using FitnessApp.Models;
using FitnessApp.Models.Dtos;

namespace FitnessApp.Services.Contracts
{
    public interface IMacrosCalculatorService
    {
        public MacrosDto CalculateMacros(MacrosCalculator data);
    }
}
