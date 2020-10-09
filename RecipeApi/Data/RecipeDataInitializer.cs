using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeApi.Data
{
    /// <summary>
    /// Initializes the database Async. on startup.
    /// </summary>
    public class RecipeDataInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public RecipeDataInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var dbContext = scope.ServiceProvider.GetRequiredService<RecipeContext>();

                //Do the initialization asynchronously
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }
        }

        // noop
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

