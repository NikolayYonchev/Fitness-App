namespace FitnessApp.Shared
{
    public class ServiceResult<T> : ServiceEmptyResult
    {
        public T? Data { get; set; }
    }
}
