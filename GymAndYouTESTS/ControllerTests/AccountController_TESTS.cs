using FluentAssertions;
using GymAndYou.DatabaseConnection;
using GymAndYou.Entities;
using GymAndYou.Entities.EntitiesInterface;
using GymAndYou.Models.DTO_Models;
using GymAndYou.Services;
using GymAndYou.TESTS.HelpTools;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Runtime.CompilerServices;

namespace GymAndYouTESTS.ControllerTests
{
    public class AccountController_TESTS : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();

        public AccountController_TESTS(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbConnection = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DbConnection>));
                        services.Remove(dbConnection);

                        services.AddSingleton(_accountService.Object);

                        services.AddDbContext<DbConnection>(option =>
                        {
                            option.UseInMemoryDatabase("DbForTesting");
                        });
                    });
                });
            _client = _factory.CreateClient();
        }

        /*
            ACCOUNT CONTROLLER TESTS
         */

        [Theory]
        [MemberData(nameof(InvalidUserDtoData))]
        public async Task CreateUser_ForInvalidQueryParameter_ReturnStatusCode400BadRequest(CreateUserDTO createUserDto)
        {
            // act
            var result = await _client.PostAsync("/register", createUserDto.ToJsonHttpContent());

            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_ForValidQueryParameter_ReturnStatusCode200Ok()
        {
            // arrange
            var createUserDto = new CreateUserDTO()
            {
                Email = "correct@email.pl",
                UserName = "Correct",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!",
                FirstName = "TestUser",
                LastName = "TestUser",
                Nationality = "TestNationality",
                RoleId = 2
            };

            // act
            var result = await _client.PostAsync("/register", createUserDto.ToJsonHttpContent());

            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task LoginUser_ForValidQueryParameter_ReturnStatusCode200Ok()
        {
            //arrange
            _accountService
                .Setup(option => option.GetJWTToken(It.IsAny<LoginUserDTO>()))
                .Returns("jwt");

            var User = new LoginUserDTO()
            {
                UserName = "username",
                Password = "password",

            }.ToJsonHttpContent();

            //act
            var result = await _client.PostAsync("/login", User);
            var jwtToken = await result.Content.ReadAsStringAsync();

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            jwtToken.Should().NotBeNull();
        }

        [Fact]
        public async Task LoginUser_ForInValidQueryParameter_ReturnStatusCode400BadRequest()
        {
            //arrange
            _accountService
                .Setup(option => option.GetJWTToken(It.IsAny<LoginUserDTO>()))
                .Returns("jwt");

            var User = new LoginUserDTO()
            {

            }.ToJsonHttpContent();

            //act
            var result = await _client.PostAsync("/login", User);

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var test = result.Content.ReadAsStream();
        }









        /*
            
            ACCOUNT CONTROLLER HELP METHOD FOR TESTS
         */

        public static IEnumerable<object[]> InvalidUserDtoData()
        {
            yield return new object[]
            {
                /*
                    Invalid password
                 */
               new CreateUserDTO()
                {
                    Email = "correct@email.pl",
                    UserName = "Correct",
                    Password = "invalidPassword",
                    ConfirmPassword = "invalidPassword@invalidPassword",
                    FirstName = "TestUser",
                    LastName = "TestUser",
                    Nationality = "TestNationality",
                    RoleId = 1
                }
            };
            /*
               Invalid email
            */
            yield return new object[]
            {
               new CreateUserDTO()
                {
                    UserName = "Correct",
                    Password = "ValidPassword123!",
                    ConfirmPassword = "ValidPassword123",
                    FirstName = "TestUser",
                    LastName = "TestUser",
                    Nationality = "TestNationality",
                    RoleId = 2
                }
            };
            /*
                Invalid username
            */
            yield return new object[]
            {
               new CreateUserDTO()
                {
                    Email = "correct@email.pl",
                    Password = "ValidPassword123!",
                    ConfirmPassword = "ValidPassword123",
                    FirstName = "TestUser",
                    LastName = "TestUser",
                    Nationality = "TestNationality",
                    RoleId = 3
                }
            };
            /*
                Invalid confirmPassword
            */
            yield return new object[]
            {
               new CreateUserDTO()
                {
                    UserName = "Correct",
                    Email = "correct@email.pl",
                    Password = "ValidPassword123!",
                    FirstName = "TestUser",
                    LastName = "TestUser",
                    Nationality = "TestNationality",
                    RoleId = 3
                }
            };
            /*
                Invalid RoleId (correct ragne is 1-3)
            */
            yield return new object[]
            {
               new CreateUserDTO()
                {
                    UserName = "Correct",
                    Email = "correct@email.pl",
                    Password = "ValidPassword123!",
                    FirstName = "TestUser",
                    LastName = "TestUser",
                    Nationality = "TestNationality",
                    RoleId = 5
                }
            };
        }
    }
}