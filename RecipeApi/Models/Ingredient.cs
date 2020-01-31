namespace RecipeApi.Models
{
    public class Ingredient
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Amount { get; set; }

        public string Unit { get; set; }
        #endregion

        #region Constructors
        public Ingredient(string name, double? amount = null, string unit = null)
        {
            Name = name;
            Amount = amount;
            Unit = unit;
        }
        #endregion
    }
}