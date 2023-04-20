using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class Gym_On_GymDTO_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Gym()
                {
                    Name= "test1",
                    Description = "test2",
                    OpeningHours = "test3",
                    Address = new Address() { City = "test4", PostalCode="test5", StreetName = "test6"},
                    AviableEquipments = new List<AviableEquipment>()
                },
                new GymDTO()
                {
                    Name = "test1",
                    Description = "test2",
                    OpeningHours = "test3",
                    City = "test4",
                    PostalCode = "test5",
                    StreetName = "test6",
                    AviableEquipments = new List<AviableEquipmentDTO>()

                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
