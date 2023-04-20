using AutoMapper.Execution;
using FluentAssertions;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.TESTS.HelpTools;
using GymAndYouTESTS.Authentication;
using GymAndYouTESTS.HelpTools;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using Xunit.Abstractions;

namespace GymAndYouTESTS.ControllerTests
{
    public class MemberController_TESTS:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _outputHelper;


        public MemberController_TESTS(WebApplicationFactory<Program> factory, ITestOutputHelper outputHelper)
        {
            _factory = factory.ConfigureAsInMemoryDataBase();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ForValidQueryParameters_ReturnStatusCode200Ok()
        {
            // arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/members");
                var responseMessage = await result.Content.ReadAsStringAsync();

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_ForNotExistingGym_ReturnStatusCode400NotFound()
        {
            // arrange
                var gymId = 999;

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/members/");
                var responseMessage = await result.Content.ReadAsStringAsync();

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForValidQueryParameters_ReturnStatusCode200Ok()
        {
            // arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/members/{memberId}");
                var responseMessage = await result.Content.ReadAsStringAsync();

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                responseMessage.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetById_ForNotExistingGym_ReturnStatusCode400NotFound()
        {
            // arrange
                var gymId = 9999;

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/members/{memberId}");
                var responseMessage = await result.Content.ReadAsStringAsync();

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForNotExisitngMember_ReturnStatusCode400NotFound()
        {
            // arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var memberId = 9999;

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/members/{memberId}");
                var responseMessage = await result.Content.ReadAsStringAsync();

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseMessage.Should();
        }

        [Fact]
        public async Task Create_ForValidQueryParameter_ReturnStatusCode201Created()
        {
            //arrange

            var gym = GymProvider.GetGym(TestUser.Test_User_Id);
            var gymId = _factory.SeedDatabase(gym);

            var memberHttpContent = GetUpsertMemberDTO().ToJsonHttpContent();

            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/members/",memberHttpContent);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
                result.Headers.Location.Should().NotBeNull();



        }

        [Fact]
        public async Task Create_ForInValidQueryParameter_ReturnStatusCode404BadRequest()
        {
            //arrange
            var gymId = 999;
            var memberHttpContent = new UpsertMemberDTO(){}.ToJsonHttpContent();

            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/members/",memberHttpContent);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_ForNotExistingGym_ReturnStatusCode400NotFound()
        {
            //arrange
                var gymId = 999;
                var memberHttpContent = GetUpsertMemberDTO().ToJsonHttpContent();

            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/members/",memberHttpContent);

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task Delete_ForValidQueryParameter_ReturnStatusCode204NoContent()
        {

            //arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}/members/{memberId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task Delete_ForNotExistingMember_ReturnStatusCode400NotFound()
        {
            //arrange

                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var memberId = 999;

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}/members/{memberId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task Delete_ForNotExistingGym_ReturnStatusCode400NotFound()
        {

           //arrange

                var gymId = 999;

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}/members/{memberId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task Update_ForValidQueryParameters_ReturnStatusCode200Ok()
        {
            // arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

                var updatedMember = GetUpsertMemberDTO().ToJsonHttpContent();
            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/members/{memberId}",updatedMember);
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_ForInValidQueryParameters_ReturnStatusCode404BadRequest()
        {
            // arrange
                var gymId = 999;
                var memberId = 999;

                var updatedMember = new UpsertMemberDTO()
                {

                    /*INVALID UpsertMemberDTO -- BAD REQUEST CAUSE*/

                }.ToJsonHttpContent();

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/members/{memberId}",updatedMember);
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ForNotExistingGym_ReturnStatusCode400NotFound()
        {
            
            // arrange
                var gymId = 999;

                var member = GetMember(gymId);
                var memberId = _factory.SeedDatabase(member);

                var updatedMember = GetUpsertMemberDTO().ToJsonHttpContent();

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/members/{memberId}",updatedMember);
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ForNotExistingMember_ReturnStatusCode400NotFound()
        {
            // arrange
                var gym = GymProvider.GetGym(TestUser.Test_User_Id);
                var gymId = _factory.SeedDatabase(gym);

                var memberId = 999;
                var updatedMember = GetUpsertMemberDTO().ToJsonHttpContent();

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/members/{memberId}",updatedMember);
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        private UpsertMemberDTO GetUpsertMemberDTO()
        {
            return new UpsertMemberDTO()
            {
                FirstName = "test2",
                LastName = "test2",
                Email= "test@test.pl",
                Phone = "600201300",
                Pesel = "68101568638",
                BirthDay = DateTime.Now,
                Sex = "male"
            };
        }
        private Members GetMember(int gymId)
        {
           return new Members()
            {
                FirstName = "test",
                LastName = "test",
                BirthDay= DateTime.Now,
                GymId = gymId,
                Email = "test@wp.pl",
                Phone = "test",
                Sex = "test",
                JoinDate = DateTime.Now,
                Pesel= "test"
            };
        }

    }
}

