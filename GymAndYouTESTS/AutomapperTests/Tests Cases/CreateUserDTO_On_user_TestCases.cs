using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Models.DTO_Models;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class CreateUserDTO_On_user_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreateUserDTO()
                {
                    Email = "test@wp.pl",
                    UserName = "test",
                    Password = "test",
                    ConfirmPassword = "test",
                    FirstName = "test",
                    LastName = "test",
                    Nationality = "test",
                    RoleId = 1
                },
                new User()
                {
                    Email = "test@wp.pl",
                    UserName = "test",
                    FirstName = "test",
                    LastName = "test",
                    Nationality = "test",
                    RoleId = 1
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
