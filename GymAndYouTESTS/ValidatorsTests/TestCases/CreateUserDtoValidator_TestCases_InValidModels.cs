using GymAndYou.Models.DTO_Models;
using System.Collections;

namespace GymAndYouTESTS.ValidatorsTests.TestCases
{
    public class CreateUserDtoValidator_TestCases_InValidModels : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Error - Already existing email in db
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "testing@account.com",
                    UserName= "Test123",
                    Password= "Testowe123!",
                    ConfirmPassword= "Testowe123!",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 1
                }
            };
            // Error - Already existing user name in db
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Test2@test.pl",
                    UserName= "test2CountCOM",
                    Password= "bbbaaacccDDD321!",
                    ConfirmPassword= "bbbaaacccDDD321!",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 2
                }
            };

            // Error - Too weak password, violating strong password policy
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Testssss@testsad12.pl",
                    UserName= "Test123",
                    Password= "test",
                    ConfirmPassword= "teStt33@T",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 3
                }
            };

            // Error - Password and confirmPassword aren't same
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email= "Test@test.pl",
                    UserName= "Test123",
                    Password= "Testowe123!",
                    ConfirmPassword= "Testo",
                    FirstName= "Test",
                    LastName= "Test",
                    Nationality = "Test",
                    RoleId = 1
                }
            };
            // Error - RoleId must in 1-3
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
                    RoleId = 999
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
