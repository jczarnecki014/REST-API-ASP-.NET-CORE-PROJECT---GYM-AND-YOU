using GymAndYou.DatabaseConnection;
using GymAndYou.Entities.EntitiesInterface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace GymAndYou.TESTS.HelpTools
{
    public static class WebApplcationFactoryTools
    {
        /// <summary>
        /// Seed the temporary InMemoryDatabase 
        /// </summary>
        /// <param name="user"></param>
        /// <returns name="WebApplicationFactory<Program>"></returns>
        public static int SeedDatabase<T>(this WebApplicationFactory<Program> factory, T obj)  where T : class, IDbEntity
        {
           var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
           using var scope = scopeFactory.CreateScope();
           var dbContext = scope.ServiceProvider.GetRequiredService<DbConnection>();

           dbContext.Set<T>().Add(obj);
           dbContext.SaveChanges();
           return obj.Id;
        }


        /// <summary>
        /// Configure WebApplicationFactory<T> database as "InMemoryDataBase" for testing purposes
        /// </summary>
        /// <param name="factory"></param>
        /// <returns>WebApplicationFactory<Program></returns>
        public static WebApplicationFactory<T> ConfigureAsInMemoryDataBase<T>(this WebApplicationFactory<T> factory) where T : class
        {
            var InMemoryDatabase = factory
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => { 
                       var dbContext =  services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DbConnection>));
                       services.Remove(dbContext);
                       services.AddDbContext<DbConnection>(option => { 
                            option.UseInMemoryDatabase("TestDataBase");
                       });
                    });
                });
            return InMemoryDatabase;
        }


        public static WebApplicationFactory<Program> MockService<T>(this WebApplicationFactory<Program> factory, Mock<T> mock) where T : class
        {
             var InMemoryDatabase = factory
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => { 
                       services.AddSingleton(mock.Object);
                    });
                });
            return InMemoryDatabase;
        }


    }
}
