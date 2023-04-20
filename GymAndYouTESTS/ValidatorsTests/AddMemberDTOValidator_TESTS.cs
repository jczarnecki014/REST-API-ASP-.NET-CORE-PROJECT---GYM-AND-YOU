using FluentAssertions;
using FluentValidation.TestHelper;
using GymAndYou.DTO_Models;
using GymAndYou.DTO_Models.Validators;

namespace GymAndYouTESTS.ValidatorsTests
{
    public class AddMemberDTOValidator_TESTS:IClassFixture<AddMemberDtoValidator>
    {
        private readonly AddMemberDtoValidator _validator;

        public AddMemberDTOValidator_TESTS(AddMemberDtoValidator validator)
        {
            _validator = validator;
        }

        public static IEnumerable<object[]> InCorrectMemberDTOExamples()
        {
            yield return new object[] 
            {
                /*Wrong FirstName - more than 50 characters*/
                new UpsertMemberDTO()
                {
                    FirstName = "MoreThan50Chars-aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong LastName - more than 50 characters*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "MoreThan50Chars-aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong Email*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test...",
                    Phone = "600600600",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PhoneNumber*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "323",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PhoneNumber*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "323524432131231",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PhoneNumber*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "00000000011111",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PESEL*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PESEL*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "00000",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PESEL*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "00000000000",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PESEL*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "12345678901",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong PESEL*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "6432134123152342",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                }
            };
            yield return new object[] 
            {
                /*Wrong SEX*/
                new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex="unnamed",
                }
            };

        }

        [Fact]
        public void Validate_ForValidModel_ReturnSuccess()
        {
            // arrange

                var correctModel = new UpsertMemberDTO()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "600600600",
                    Pesel = "68101568638",
                    BirthDay = DateTime.Now,
                    Sex= "male",
                };

            // act
                var result = _validator.TestValidate(correctModel);

            // assert
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(InCorrectMemberDTOExamples))]
        public void Validate_ForInValidModel_ReturnError(UpsertMemberDTO model)
        {

            // act
               var result = _validator.TestValidate(model);
            // assert
                result.ShouldHaveAnyValidationError();
        }
    }
}
