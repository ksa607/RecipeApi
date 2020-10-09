using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApi.Models
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetByAsync(int id);
        Task<(bool, Recipe)> TryGetRecipeAsync(int id);
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<IEnumerable<Recipe>> GetByAsync(string name = null, string chef = null, string ingredientName = null);
        void Add(Recipe recipe);
        void Delete(Recipe recipe);
        void Update(Recipe recipe);
        Task SaveChangesAsync();
    }
}

