using System.Collections.Generic;
using System.Linq;

namespace RecipeApi.Models
{
    public class Customer
    {
        #region Properties
        //add extra properties if needed
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerFavorite> Favorites { get; private set; }

        public IEnumerable<Recipe> FavoriteRecipes => Favorites.Select(f => f.Recipe);
        #endregion

        #region Constructors
        public Customer()
        {
            Favorites = new List<CustomerFavorite>();
        }
        #endregion

        #region Methods
        public void AddFavoriteRecipe(Recipe recipe)
        {
            Favorites.Add(new CustomerFavorite() { RecipeId = recipe.Id, CustomerId = CustomerId, Recipe = recipe, Customer = this });
        }
        #endregion
    }
}

