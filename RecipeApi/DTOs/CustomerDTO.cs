using RecipeApi.Models;
using System.Collections.Generic;

namespace RecipeApi.DTOs
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }

        public CustomerDTO() { }

        public CustomerDTO(Customer customer) : this()
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            Recipes = customer.FavoriteRecipes;
        }
    }
}
