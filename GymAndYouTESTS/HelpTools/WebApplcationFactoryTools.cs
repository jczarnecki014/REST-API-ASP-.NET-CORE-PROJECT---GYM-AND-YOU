using FluentAssertions.Common;
using GymAndYou.DatabaseConnection;
using GymAndYou.Entities.EntitiesInterface;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Linq.Expressions;

namespace GymAndYou.TESTS.HelpTools
{
    public static class WebApplcationFactoryTools
    {
        private static Type test;
        /// <summary>
        /// Seed the temporary InMemoryDatabase by default data
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
        /// Configure WebApplicationFactory<T> database as "InMemoryDataBase" for testing purposes ( remove real dbConnection and create inMemoryDatabase)
        /// </summary>
        /// <param name="factory"></param>
        /// <returns>WebApplicationFactory<Program></returns>
        public static WebApplicationFactory<Program> ConfigureAsInMemoryDataBase(this WebApplicationFactory<Program> factory)
        {

            var updatedFacotry = factory.ConfigureServices(services => {
                var dbContext =  services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DbConnection>));

                services.Remove(dbContext);

                services.AddDbContext<DbConnection>(option => { 
                    option.UseInMemoryDatabase("TestDataBase");
                });
            });

           return updatedFacotry;
        }

        /// <summary>
        /// This method allow you to mock the default servies set in project
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="mock"></param>
        /// <returns></returns>
        public static WebApplicationFactory<Program> MockService<T>(this WebApplicationFactory<Program> factory, Mock<T> mock) where T : class
        {
           var updatedFacotry = factory.ConfigureServices(services => {
                services.AddSingleton(typeof(T),mock.Object);
            });

           return updatedFacotry;
        }


        /// <summary>
        /// For testing purposes, this method enable you change dafualt authentication policy. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <returns>this WebApplicationFactory<Program></returns>
        /// <example>
        ///     Use example:
        ///     <code>
        ///         WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>()
        ///         factory.SetPolicy<FakePolicyEvaluator>();
        ///     </code>
        /// </example>

        public static WebApplicationFactory<Program> SetPolicy<T>(this WebApplicationFactory<Program> factory) where T: class,IPolicyEvaluator
        {
           var updatedFacotry = factory.ConfigureServices(services => {
                services.AddSingleton<IPolicyEvaluator,T>();
           });

           return updatedFacotry;
        }

        /// <summary>
        /// Method aviable only in this class, it major task is configuring services in inMemoryDatabase
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static WebApplicationFactory<Program> ConfigureServices(this WebApplicationFactory<Program> factory,Action<IServiceCollection> action) 
        {
            var InMemoryDatabase = factory
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(action);
                });
            return InMemoryDatabase;
        }
    }
}
