using GymAndYou.DatabaseConnection;
using GymAndYou.Entities.EntitiesInterface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GymAndYou.TESTS.HelpTools
{
    public static class WebApplcationFactorySeeder
    {
        /// <summary>
        /// Seed the temporary InMemoryDatabase 
        /// </summary>
        /// <param name="user"></param>
        /// <returns name="WebApplicationFactory<Program>"></returns>
        public static WebApplicationFactory<Program> SeedDatabase<T>(this WebApplicationFactory<Program> factory, T obj) where T : class, IDbEntity
        {
           var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
           using var scope = scopeFactory.CreateScope();
           var dbContext = scope.ServiceProvider.GetRequiredService<DbConnection>();

           var dbSet = dbContext.Set<T>().ToList();
           dbSet.Add(obj);
           dbContext.SaveChanges();
           return factory;
        }
    }
}
