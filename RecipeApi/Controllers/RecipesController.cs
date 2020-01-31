using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
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
        /// <summary>
        /// Get all recipes ordered by name
        /// </summary>
        /// <returns>array of recipes</returns>

        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeRepository.GetAll().OrderBy(r => r.Name);
        }

        // GET: api/Recipes/5
        /// <summary>
        /// Get the recipe with given id
        /// </summary>
        /// <param name="id">the id of the recipe</param>
        /// <returns>The recipe</returns>
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            Recipe recipe = _recipeRepository.GetBy(id);
            if (recipe == null) return NotFound();
            return recipe;
        }

        // POST: api/Recipes
        /// <summary>
        /// Adds a new recipe
        /// </summary>
        /// <param name="recipe">the new recipe</param>
        [HttpPost]
        public ActionResult<Recipe> PostRecipe(Recipe recipe)
        {
            _recipeRepository.Add(recipe);
            _recipeRepository.SaveChanges();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // PUT: api/Recipes/5
        /// <summary>
        /// Modifies a recipe
        /// </summary>
        /// <param name="id">id of the recipe to be modified</param>
        /// <param name="recipe">the modified recipe</param>
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
        /// <summary>
        /// Deletes a recipe
        /// </summary>
        /// <param name="id">the id of the recipe to be deleted</param>

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