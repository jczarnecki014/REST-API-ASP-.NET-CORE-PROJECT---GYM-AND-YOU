using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class UpsertMemberDTO_On_Members_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpsertMemberDTO()
                {
                    FirstName= "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Sex = "test",
                    Phone= "test",
                    Pesel = "test",
                },
                new Members()
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.pl",
                    Phone = "test",
                    Pesel = "test",
                    Sex = "test",
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
