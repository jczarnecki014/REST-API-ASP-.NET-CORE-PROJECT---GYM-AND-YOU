using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class UpsertEquipmentDTO_On_AviableEquipment_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpsertEquipmentDTO()
                {
                    Name= "test1",
                    Description = "test2",
                    BodyPart = "Legs",
                    MaxWeight = 120
                },
                new AviableEquipment()
                {
                    Name = "test1",
                    Description = "test2",
                    BodyPart = "Legs",
                    MaxWeight = 120,
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
