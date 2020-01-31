using Microsoft.EntityFrameworkCore;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Recipe> GetAll()
        {
            return _recipes.ToList();
        }

        public Recipe GetBy(int id)
        {
            return _recipes.Include(r => r.Ingredients).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetRecipe(int id, out Recipe recipe)
        {
            recipe = _context.Recipes.Include(t => t.Ingredients).FirstOrDefault(t => t.Id == id);
            return recipe != null;
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
     }
}
