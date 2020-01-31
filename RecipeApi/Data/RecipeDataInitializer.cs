using Microsoft.AspNetCore.Identity;

namespace RecipeApi.Data
{
    public class RecipeDataInitializer
    {
        private readonly RecipeContext _dbContext;

        public RecipeDataInitializer(RecipeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext               
            }
        }

             }
}

