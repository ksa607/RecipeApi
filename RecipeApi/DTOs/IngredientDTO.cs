using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DTOs
{
    public class IngredientDTO
    {
        [Required]
        public string Name { get; set; }

        public double? Amount { get; set; }

        public string Unit { get; set; }
    }
}
