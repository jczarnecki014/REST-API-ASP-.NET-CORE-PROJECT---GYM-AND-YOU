using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class Members_On_MembersDTO_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Members()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "test",
                    BirthDay = DateTime.Now,
                    Sex = "test",
                    JoinDate = DateTime.Now,
                    GymId = 999
                },
                new MembersDTO()
                {
                    FirstName= "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Sex = "test"
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
