using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecipeApi.Models
{
    public class Recipe
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string Chef { get; set; }

        public ICollection<Ingredient> Ingredients { get; private set; }
        #endregion

        #region Constructors
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Created = DateTime.Now;
        }

        public Recipe(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region Methods
        public void AddIngredient(Ingredient ingredient) => Ingredients.Add(ingredient);

        public Ingredient GetIngredient(int id) => Ingredients.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}