namespace FitnessApp.Models.Dtos
{
    public class MealDto
    {
        public MealDto()
        {
            ProductNames = new HashSet<string>();
            //SetMacros();
        }
        public int MealId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        /*public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }*/
        public IEnumerable<string> ProductNames { get; set; }

       /* private void SetMacros()
        {
            
            foreach (var product in Products)
            {
                Calories = product.Calories;
                Protein = product.Protein;
                Fats = product.Fats;
                Carbs = product.Carbohydrates;
            }
        }*/
    }
}
