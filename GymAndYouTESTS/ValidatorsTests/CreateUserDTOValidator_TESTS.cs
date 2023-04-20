using FluentAssertions;
using FluentValidation.TestHelper;
using GymAndYou.DatabaseConnection;
using GymAndYou.Entities;
using GymAndYou.Models.DTO_Models;
using GymAndYou.Models.DTO_Models.Validators;
using GymAndYouTESTS.ValidatorsTests.TestCases;
using Microsoft.EntityFrameworkCore;

namespace GymAndYouTESTS.ValidatorsTests
{
    public class CreateUserDTOValidator_TESTS
    {
        private readonly DbConnection _db;
        private readonly CreateUserDTOValidator _validator;
        public CreateUserDTOValidator_TESTS()
        {
            DbContextOptionsBuilder<DbConnection> builder = new DbContextOptionsBuilder<DbConnection>();
            builder.UseInMemoryDatabase("GymDB");
           _db = new DbConnection(builder.Options);
           _validator = new CreateUserDTOValidator(_db);
           SeedDatabase();
        }

        private void SeedDatabase()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "testing@account.com",
                    UserName = "testCountCOM",
                    FirstName= "Test",
                    LastName = "test",
                    Nationality = "test",
                    PasswordHash = "XXXX"
                },
                new User()
                {
                    Email = "testing2@account.com",
                    UserName = "test2CountCOM",
                    FirstName= "Test",
                    LastName = "test",
                    Nationality = "test",
                    PasswordHash = "XXXX"
                }
            };

            _db.Users.AddRange(users);
            _db.SaveChanges();
        }

        [Theory]
        [ClassData(typeof(CreateUserDtoValidator_TestCases_ValidModels))]
        public void Validation_ForValidQueryParameters_ReturnSuccess(CreateUserDTO model)
        {

            // act
               var result = _validator.TestValidate(model);

            // assert
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [ClassData(typeof(CreateUserDtoValidator_TestCases_InValidModels))]
        public void Validation_ForInvalidQueryParameters_ReturnError(CreateUserDTO model)
        {
            // act
                var result = _validator.TestValidate(model);

            // assert
                result.ShouldHaveAnyValidationError();
        }
    }
}
