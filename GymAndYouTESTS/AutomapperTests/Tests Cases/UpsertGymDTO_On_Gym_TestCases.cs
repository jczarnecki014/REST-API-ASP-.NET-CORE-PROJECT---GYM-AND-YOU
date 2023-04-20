using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class UpsertGymDTO_On_Gym_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpsertGymDTO()
                {
                    Name = "test",
                    Description = "test",
                    OpeningHours = "test",
                    City = "test",
                    StreetName = "test",
                    PostalCode = "test",
                },
                new Gym()
                {
                    Name= "test",
                    Description = "test",
                    OpeningHours = "test",
                    Address = new Address() { City = "test", PostalCode="test", StreetName = "test"},
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
