using FluentAssertions;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.TESTS.HelpTools;
using GymAndYouTESTS.HelpTools;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace GymAndYouTESTS.ControllerTests
{
    public class EquipmentService_tests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public EquipmentService_tests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.ConfigureAsInMemoryDataBase();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ForExistingGym_ReturnStatus200Ok()
        {
            // arrange 
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/equipment/");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_ForNotExistingGym_ReturnStatus403NotFound()
        {
            // arrange
                var gymId = 9999999;
            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/equipment/");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForValidQueryParameter_ReturnStatusCode200Ok()
        {
            // arrange
                
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                int equipmentId = CreateEquipment(gymId);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/equipment/{equipmentId}");
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ForNotExistingGym_ReturnStatusCode404NotFound()
        {
            // arrange
                int gymId = 9999;
                int equipmentId = 2;

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/equipment/{equipmentId}");
                var responseBody = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseBody.Should().Be("Gym with this ID doesn't exist");
        }

        [Fact]
        public async Task GetById_ForNotExistingEquipment_ReturnStatusCode404NotFound()
        {
            // arrange
                int equipmentId = 99999999;

                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

            // act
                var result = await _client.GetAsync($"/api/gym/{gymId}/equipment/{equipmentId}");
                var responseBody = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseBody.Should().Be("Equipment with this ID doesn't exist or doesn't exist in this gym");
        }

        [Fact]
        public async Task CreateEquipment_ForValidQueryParameters_ReturnStatusCode200Ok()
        {
            // arrange 
                
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                UpsertEquipmentDTO newEquipment = new()
                {
                    Name = "test",
                    Description = "test",
                    BodyPart = "Legs",
                    MaxWeight= 100,
                }; 

            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/equipment/",newEquipment.ToJsonHttpContent());
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
                result.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateEquipment_ForNotExistingGym_ReturnStatusCode404NotFound()
        {
            // arrange 
                UpsertEquipmentDTO newEquipment = new()
                {
                    Name = "test",
                    Description = "test",
                    BodyPart = "Legs",
                    MaxWeight= 100,
                }; 

                int gymId = 403;
            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/equipment/",newEquipment.ToJsonHttpContent());
                var responseText = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseText.Should().Be("Gym with this ID doesn't exist");
        }

         [Fact]
        public async Task CreateEquipment_ForInValidQueryParameters_ReturnStatusCode400BadRequest()
        {
            // arrange 
                
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                UpsertEquipmentDTO newEquipment = new()
                {
                    // invalid
                }; 

            // act
                var result = await _client.PostAsync($"/api/gym/{gymId}/equipment/",newEquipment.ToJsonHttpContent());
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
         }

        [Fact]
        public async Task RemoveEquipment_ForExistingGymAndEquipment_ReturnStatusCode204NoContent()
        {
            // arrange 
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                int equipmentId = CreateEquipment(gymId);

            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}/equipment/{equipmentId}");

            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task RemoveEquipment_ForEquipmentNotExistingInGym_ReturnStatusCode404NotFound()
        {
            // arrange
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);
                int equipmentId = 999;
            // act
                var result = await _client.DeleteAsync($"/api/gym/{gymId}/equipment/{equipmentId}");
                var responseText = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseText.Should().Be("Equipment with this ID doesn't exist or doesn't exist in this gym");
        }

        [Fact]
        public async Task RemoveEquipment_ForNotExistingGym_ReturnStatusCode404NotFound()
        {
            // arrange
                int notExistingGymId = 222;
                int notExistingEquipmentId = 999;
            // act
                var result = await _client.DeleteAsync($"/api/gym/{notExistingGymId}/equipment/{notExistingEquipmentId}");
                var responseText = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseText.Should().Be("Gym with this ID doesn't exist");
        }

        [Fact]
        public async Task UpdateEquipment_ForExistingGymAndEquipmentAndValidQueryParameter_ReturnStatusCode200Ok()
        {
            // arrange
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                int equipmentId = CreateEquipment(gymId);

                UpsertEquipmentDTO equipmentDTO = new UpsertEquipmentDTO()
                {
                    Name = "test",
                    Description = "test",
                    BodyPart = "Legs",
                    MaxWeight = 150
                };

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/equipment/{equipmentId}",equipmentDTO.ToJsonHttpContent());
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateEquipment_ForNotExistingEquipmentInGym_ReturnStatusCode404NotFound()
        {
            // arrange
                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);
                int notExistingEquipmentId = 99999;
                UpsertEquipmentDTO equipmentDTO = new UpsertEquipmentDTO()
                {
                    Name = "test",
                    BodyPart = "Legs",
                    Description = "test",
                    MaxWeight = 100,
                };

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/equipment/{notExistingEquipmentId}",equipmentDTO.ToJsonHttpContent());
                var responseMessage = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseMessage.Should().Be("Equipment with this ID doesn't exist or doesn't exist in this gym");
        }

        [Fact]
        public async Task UpdateEquipment_ForNotExisingGym_ReturnStatusCode404NotFound()
        {
            // arrange
                int notExistingGymId = 5555;
                int notExistingEquipmentId = 99999;
                UpsertEquipmentDTO equipmentDTO = new UpsertEquipmentDTO()
                {
                    Name = "test",
                    BodyPart = "Legs",
                    Description = "test",
                    MaxWeight = 100,
                };

            // act
                var result = await _client.PutAsync($"/api/gym/{notExistingGymId}/equipment/{notExistingEquipmentId}",equipmentDTO.ToJsonHttpContent());
                var responseMessage = await result.Content.ReadAsStringAsync();
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
                responseMessage.Should().Be("Gym with this ID doesn't exist");
        }

        [Fact]
        public async Task UpdateEquipment_ForInvalidQueryParameter_ReturnStatusCode400BadRequest()
        {
            // arrange
                UpsertEquipmentDTO invalidEquipmentDTO = new()
                { 
                    // Invalid 
                };

                var gym = GymProvider.GetGym();
                var gymId = _factory.SeedDatabase(gym);

                int equipmentId = CreateEquipment(gymId);

            // act
                var result = await _client.PutAsync($"/api/gym/{gymId}/equipment/{equipmentId}",invalidEquipmentDTO.ToJsonHttpContent());
            // assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


        private int CreateEquipment(int gymId)
        {
            AviableEquipment equipment= new AviableEquipment()
                {
                    Name = "test",
                    Description = "test",
                    MaxWeight = 100,
                    BodyPart = "test",
                    GymId = gymId
                };
            int equipmentId = _factory.SeedDatabase<AviableEquipment>(equipment);
            return equipmentId;
        }
    }
}
