using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using System.Collections;

namespace GymAndYouTESTS.AutomapperTests.Tests_Cases
{
    public class AviableEquipment_On_AviableEquipmentDTO_TestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new AviableEquipment()
                {
                    Name= "test1",
                    Description = "test2",
                    BodyPart = "test3",
                    MaxWeight = 100,
                    GymId = 999
                },
                new AviableEquipmentDTO()
                {
                    Name = "test1",
                    Description = "test2",
                    BodyPart = "test3"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
