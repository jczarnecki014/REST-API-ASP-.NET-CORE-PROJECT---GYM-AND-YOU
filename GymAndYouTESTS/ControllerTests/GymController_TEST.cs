using FluentAssertions;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.TESTS.HelpTools;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymAndYouTESTS.ControllerTests
{
    public class GymController_TEST:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public GymController_TEST(WebApplicationFactory<Program> factory)
        {
            _factory = factory.ConfigureAsInMemoryDataBase();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetById_ForExistingGym_ReturnStatusCode200Ok()
        {
            // arrange
                Gym gym = new Gym()
                {
                    Name= "test",
                    Description = "test",
                    OpeningHours = "12-24",
                        Address = new Address()
                        {
                           City = "test",
                           StreetName = "test",
                           PostalCode = "test",
                        }
                };
                var gymId = _factory.SeedDatabase(gym);
            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}");
                var responseContent = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                responseContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetById_ForNotExistingGym_ReturnStatusCode404NotFound()
        {
            // arrange
                var notExistingGym = 999999;

            // act
                var result = await _client.GetAsync($"/api/gym/{notExistingGym}");
            
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }   

        [Fact]
        public async Task GetAll_ForValidQueryParameters_ReturnStatusCode200Ok()
        {
            // arrange
                GymQuery query = new GymQuery()
                {
                    SortBy = "Name",
                    PageSize = 5,
                    PageNumber = 2
                };
            // act
                var result = await _client.GetAsync($"/api/gym?SortBy={query.SortBy}&PageSize={query.PageSize}&PageNumber={query.PageNumber}");
                var responseContent = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                responseContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAll_ForInvalidQueryParameters_ReturnStatusCode400BadRequest()
        {
            // act
                var result = await _client.GetAsync($"/api/gym");
                var responseContent = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
                responseContent.Should().NotBeNullOrEmpty();
        }
    }
}
