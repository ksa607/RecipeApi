using Microsoft.AspNetCore.Identity;
using RecipeApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Data
{
    public class RecipeDataInitializer
    {
        private readonly RecipeContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeDataInitializer(RecipeContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext         
                Customer customer = new Customer { Email = "recipemaster@hogent.be", FirstName = "Adam", LastName = "Master" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111");
                Customer student = new Customer { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                student.AddFavoriteRecipe(_dbContext.Recipes.First());
                await CreateUser(student.Email, "P@ssword1111");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}

