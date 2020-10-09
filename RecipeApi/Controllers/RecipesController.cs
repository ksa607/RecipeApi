using Microsoft.AspNetCore.Mvc;
using RecipeApi.DTOs;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Task<IEnumerable<Recipe>> GetRecipes(string name = null, string chef = null, string ingredientName = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(chef) && string.IsNullOrEmpty(ingredientName))
                return _recipeRepository.GetAllAsync();
            return _recipeRepository.GetByAsync(name, chef, ingredientName);
        }

        // GET: api/Recipes/5
        /// <summary>
        /// Get the recipe with given id
        /// </summary>
        /// <param name="id">the id of the recipe</param>
        /// <returns>The recipe</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            Recipe recipe = await _recipeRepository.GetByAsync(id);
            if (recipe == null) return NotFound();
            return recipe;
        }

        // POST: api/Recipes
        /// <summary>
        /// Adds a new recipe
        /// </summary>
        /// <param name="recipe">the new recipe</param>
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(RecipeDTO recipe)
        {
            Recipe recipeToCreate = new Recipe() { Name = recipe.Name, Chef = recipe.Chef };
            foreach (var i in recipe.Ingredients)
                recipeToCreate.AddIngredient(new Ingredient(i.Name, i.Amount, i.Unit));
            _recipeRepository.Add(recipeToCreate);
            await _recipeRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipeToCreate.Id }, recipeToCreate);
        }

        // PUT: api/Recipes/5
        /// <summary>
        /// Modifies a recipe
        /// </summary>
        /// <param name="id">id of the recipe to be modified</param>
        /// <param name="recipe">the modified recipe</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            _recipeRepository.Update(recipe);
            await _recipeRepository.SaveChangesAsync();
            return NoContent(); // Consider returning ok.
        }

        // DELETE: api/Recipes/5
        /// <summary>
        /// Deletes a recipe
        /// </summary>
        /// <param name="id">the id of the recipe to be deleted</param>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            Recipe recipe = await _recipeRepository.GetByAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            _recipeRepository.Delete(recipe);
            await _recipeRepository.SaveChangesAsync();
            return NoContent();  // Consider returning ok.
        }

        /// <summary>
        /// Get an ingredient for a recipe
        /// </summary>
        /// <param name="id">id of the recipe</param>
        /// <param name="ingredientId">id of the ingredient</param>
        [HttpGet("{id}/ingredients/{ingredientId}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id, int ingredientId)
        {
            var (isFound, recipe) = await _recipeRepository.TryGetRecipeAsync(id);

            if (!isFound)
            {
                return NotFound();
            }
            Ingredient ingredient = recipe.GetIngredient(ingredientId);
            if (ingredient == null)
                return NotFound();
            return ingredient;
        }

        /// <summary>
        /// Adds an ingredient to a recipe
        /// </summary>
        /// <param name="id">the id of the recipe</param>
        /// <param name="ingredient">the ingredient to be added</param>
        [HttpPost("{id}/ingredients")]
        public async Task<ActionResult<Ingredient>> PostIngredient(int id, IngredientDTO ingredient)
        {
            var (isFound, recipe) = await _recipeRepository.TryGetRecipeAsync(id);

            if (!isFound)
            {
                return NotFound();
            }
            var ingredientToCreate = new Ingredient(ingredient.Name, ingredient.Amount, ingredient.Unit);
            recipe.AddIngredient(ingredientToCreate);
            await _recipeRepository.SaveChangesAsync();
            return CreatedAtAction("GetIngredient", new { id = recipe.Id, ingredientId = ingredientToCreate.Id }, ingredientToCreate);
        }
    }
}
