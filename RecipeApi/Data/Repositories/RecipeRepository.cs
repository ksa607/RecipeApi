using Microsoft.EntityFrameworkCore;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _context;
        private readonly DbSet<Recipe> _recipes;

        public RecipeRepository(RecipeContext dbContext)
        {
            _context = dbContext;
            _recipes = dbContext.Recipes;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _recipes.Include(r => r.Ingredients).ToListAsync();
        }

        public Task<Recipe> GetByAsync(int id)
        {
            return _recipes.Include(r => r.Ingredients).SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<(bool,Recipe)> TryGetRecipeAsync(int id)
        {
            // Consider using a Guard Pattern instead of this function. (no out variables are possible in async functions).
            var recipe = await _context.Recipes.Include(t => t.Ingredients).FirstOrDefaultAsync(t => t.Id == id);
            return (recipe != null, recipe);
        }

        public void Add(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void Update(Recipe recipe)
        {
            _context.Update(recipe);
        }

        public void Delete(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> GetByAsync(string name = null, string chef = null, string ingredientName = null)
        {
            return await _recipes.Include(r => r.Ingredients)
                    .Where(r => r.Name.IndexOf(name) >= 0 || string.IsNullOrEmpty(name))
                    .Where(r => r.Chef==chef || string.IsNullOrEmpty(chef))
                    .Where(r => r.Ingredients.Any(i => i.Name==ingredientName) || string.IsNullOrEmpty(ingredientName))
                    .OrderBy(r => r.Name).ToListAsync();
        }
    }
}
