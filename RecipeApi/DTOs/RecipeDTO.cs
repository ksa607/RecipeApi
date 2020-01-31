using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DTOs
{
    public class RecipeDTO
    {
        [Required]
        public string Name { get; set; }

        public string Chef { get; set; }

        public IList<IngredientDTO> Ingredients { get; set; }
    }
}
