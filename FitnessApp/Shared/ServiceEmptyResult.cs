using FitnessApp.Models.Enums;

namespace FitnessApp.Shared
{
    public class ServiceEmptyResult
    {
        public bool Success { get; set; }
        public ErrorMessage? ErrorMessage { get; set; }
    }
}
