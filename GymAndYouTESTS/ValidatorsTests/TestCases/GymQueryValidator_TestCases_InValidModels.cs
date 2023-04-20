using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.StaticData;
using System.Collections;

namespace GymAndYouTESTS.ValidatorsTests.TestCases
{
    public class GymQueryValidator_TestCases_InValidModels : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            /* Invalid SortBy field - Static.SortByAllowedColumns doesn't contain value = testXAXAXAXAXATESTXAXAXATESTXXXXXDX */
            yield return new object[]
            {
                new GymQuery()
                {
                    SortBy = "testXAXAXAXAXATESTXAXAXATESTXXXXXDX",
                    PageSize = Static.AviablePageSizes[0],
                    PageNumber = Static.MimimumPageNumber + 1,
                    SortDirection = GymAndYou.Models.Query_Models.SortDirection.Asc
                }
            };

            /* Invalid PageSize field - Static.AviablePageSizes doesn't contain value = 999999999 */
            yield return new object[]
            {
                new GymQuery()
                {
                    SortBy = Static.SortByAllowedColumns[0],
                    PageSize = 999999999,
                    PageNumber = Static.MimimumPageNumber + 1,
                    SortDirection = GymAndYou.Models.Query_Models.SortDirection.Asc
                }
            };

            /* Invalid PageNumber field - value might not be less than Static.MimimumPageNumber  */
            yield return new object[]
            {
                new GymQuery()
                {
                    SortBy = Static.SortByAllowedColumns[0],
                    PageSize = Static.AviablePageSizes[0],
                    PageNumber = Static.MimimumPageNumber - 5,
                    SortDirection = GymAndYou.Models.Query_Models.SortDirection.Asc
                }
            };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
