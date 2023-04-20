using FluentAssertions;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.TESTS.HelpTools;
using GymAndYouTESTS.Authentication;
using GymAndYouTESTS.HelpTools;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
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
            _factory = factory.ConfigureAsInMemoryDataBase().SetPolicy<FakePolicyEvaluator>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetById_ForExistingGym_ReturnStatusCode200Ok()
        {
            // arrange
                Gym gym = GymProvider.GetGym(TestUser.Test_User_Id);
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

        [Fact]
        public async Task CreateGym_ForValidQueryParameters_ReturnStatusCode201Created()
        { 
            // arrange
                var gymDto = GetUpsertGymDTO().ToJsonHttpContent();

            // act
                var result = await _client.PostAsync("/api/gym",gymDto);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateGym_ForInValidQueryParameters_ReturnStatusCode400BadRequest()
        { 
            // arrange
                var gymDto = new UpsertGymDTO() 
                { 
                }.ToJsonHttpContent();

            // act
                var result = await _client.PostAsync("/api/gym",gymDto);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteGym_ForValidQueryParameters_ReturnStatusCode204NoContent()
        {
            // arrange
            var gym = GymProvider.GetGym(TestUser.Test_User_Id);
            var gymId = _factory.SeedDatabase(gym);

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteGym_ForNotExistingGym_ReturnStatusCode404NotFound()
        {
            // arrange
                var gymId = 9999;

            // act
               var result = await _client.DeleteAsync($"/api/gym/{gymId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteGym_ForNotAuthorizedUser_ReturnStatusCode403Forbidden()
        {
            // arrange

            var noExistingUser = 9999;
                /*var testedUser = TestUser.Test_User_Id;  <----- this is logged user, we don't use him */

            var gym = GymProvider.GetGym(noExistingUser);
            var gymId = _factory.SeedDatabase(gym);

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task UpdateGym_ForValidQueryParameters_ReturnStatusCode200Ok()
        { 
            var gym = GymProvider.GetGym(TestUser.Test_User_Id);
            var gymId = _factory.SeedDatabase(gym);

            var gymUpdate = GetUpsertGymDTO().ToJsonHttpContent();


            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}",gymUpdate);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateGym_ForNotExistingGym_ReturnStatusCode400NotFound()
        { 

            var gymId = 9999;

            var gymUpdate = GetUpsertGymDTO().ToJsonHttpContent();


            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}",gymUpdate);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateGym_ForNotAuthorizedUser_ReturnStatusCode403Forbidden()
        { 

            //arrange

            var noExistingUser = 9999;

            var gym = GymProvider.GetGym(noExistingUser);
            var gymId = _factory.SeedDatabase(gym);

            var gymUpdate = GetUpsertGymDTO().ToJsonHttpContent();


            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}",gymUpdate);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task UpdateGym_ForValidQueryParameters_ReturnStatusCode404BadRequest()
        { 
            var gym = GymProvider.GetGym(TestUser.Test_User_Id);
            var gymId = _factory.SeedDatabase(gym);

            var gymUpdate = new UpsertGymDTO().ToJsonHttpContent();


            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}",gymUpdate);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        private UpsertGymDTO GetUpsertGymDTO()
        {
            return new UpsertGymDTO()
            {
                Name="test2",
                Description="test2",
                OpeningHours="test2",
                City = "test2",
                StreetName="test2",
                PostalCode="test2"
            };
        }

    }
}
