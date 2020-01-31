using System.Collections.Generic;

namespace RecipeApi.Models
{
    public interface IRecipeRepository
    {
        Recipe GetBy(int id);
        bool TryGetRecipe(int id, out Recipe recipe);
        IEnumerable<Recipe> GetAll();
        IEnumerable<Recipe> GetBy(string name = null, string chef = null, string ingredientName = null);
        void Add(Recipe recipe);
        void Delete(Recipe recipe);
        void Update(Recipe recipe);
        void SaveChanges();
    }
}

