using System.Collections.Generic;

namespace RecipeApi.Models
{
    public interface IRecipeRepository
    {
        Recipe GetBy(int id);
        bool TryGetRecipe(int id, out Recipe recipe);
        IEnumerable<Recipe> GetAll();
        void Add(Recipe recipe);
        void Delete(Recipe recipe);
        void Update(Recipe recipe);
        void SaveChanges();
    }
}

