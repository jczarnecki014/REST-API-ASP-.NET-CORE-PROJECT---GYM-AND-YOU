using GymAndYou.Models.DTO_Models;
using System.Collections;

namespace GymAndYouTESTS.ValidatorsTests.TestCases
{
    public class CreateUserDtoValidator_TestCases_ValidModels : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Test@test.pl",
                    UserName= "Test123",
                    Password= "Testowe123!",
                    ConfirmPassword= "Testowe123!",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 1
                }
            };
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Test2@test.pl",
                    UserName= "Test123523421331",
                    Password= "bbbaaacccDDD321!",
                    ConfirmPassword= "bbbaaacccDDD321!",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 2
                }
            };
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Testssss@testsad12.pl",
                    UserName= "Test123",
                    Password= "teStt33@T",
                    ConfirmPassword= "teStt33@T",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 3
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
