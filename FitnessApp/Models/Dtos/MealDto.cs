namespace FitnessApp.Models.Dtos
{
    public class MealDto
    {
        public MealDto()
        {
            Products = new HashSet<Product>();
        }
        public int MealId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
