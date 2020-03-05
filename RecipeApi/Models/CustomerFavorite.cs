namespace RecipeApi.Models
{
    public class CustomerFavorite
    {
        #region Properties
        public int CustomerId { get; set; }

        public int RecipeId { get; set; }

        public Customer Customer { get; set; }

        public Recipe Recipe { get; set; }
        #endregion
    }
}
