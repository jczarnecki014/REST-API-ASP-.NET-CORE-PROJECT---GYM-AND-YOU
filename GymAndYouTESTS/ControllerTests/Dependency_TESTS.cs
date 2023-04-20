using FluentAssertions;
using GymAndYou.Controllers;
using GymAndYou.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GymAndYouTESTS.ControllerTests
{
    public class Dependency_TESTS:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly List<Type> _contorllerTypes;
        public Dependency_TESTS(WebApplicationFactory<Program> factory)
        {
            _contorllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(c => c.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            _factory = factory.WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => {
                    _contorllerTypes.ForEach(c => services.AddScoped(c));
                });
            });
        }

        [Fact]
        public void ConfigureServices_ForControllers_RegisterAllDependencies()
        {
            // arrange
                var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
                using var scope = scopeFactory.CreateScope();
                
                _contorllerTypes.ForEach(e => { 
                    var controller = scope.ServiceProvider.GetService(e);
                    controller.Should().NotBeNull();
                });
        }
    }


}
