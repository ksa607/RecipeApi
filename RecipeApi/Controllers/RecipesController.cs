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
        
        // GET: api/Recipes/id
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            Recipe recipe = _recipeRepository.GetBy(id);
            if (recipe == null) return NotFound();
            return recipe;
        }

        // POST: api/Recipes
        [HttpPost]
        public ActionResult<Recipe> PostRecipe(Recipe recipe)
        {
            _recipeRepository.Add(recipe);
            _recipeRepository.SaveChanges();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public IActionResult PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            _recipeRepository.Update(recipe);
            _recipeRepository.SaveChanges();
            return NoContent();
        }


        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            Recipe recipe = _recipeRepository.GetBy(id);
            if (recipe == null)
            {
                return NotFound();
            }
            _recipeRepository.Delete(recipe);
            _recipeRepository.SaveChanges();
            return NoContent();
        }

    }
}