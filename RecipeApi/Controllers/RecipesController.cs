using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository context)
        {
            _recipeRepository = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeRepository.GetAll().OrderBy(r => r.Name);
        }
    }
}