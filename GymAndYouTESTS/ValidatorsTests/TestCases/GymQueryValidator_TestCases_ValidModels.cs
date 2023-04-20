using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.StaticData;
using System.Collections;

namespace GymAndYouTESTS.ValidatorsTests.TestCases
{
    public class GymQueryValidator_TestCases_ValidModels : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            List<object[]> items = new List<object[]>();

            Static.SortByAllowedColumns.ForEach((column) => 
            {
                items.Add
                (
                    new object[]
                    {
                        new GymQuery() 
                        {
                            SortBy = column,
                            PageSize = Static.AviablePageSizes[0],
                            PageNumber = Static.MimimumPageNumber + 1,
                            SortDirection = GymAndYou.Models.Query_Models.SortDirection.Asc
                        }
                    }
                );;
            });
            Static.AviablePageSizes.ForEach((PageSize) => 
            {
                   items.Add
                   ( 
                        new object[]
                        {
                            new GymQuery() 
                            {
                                SortBy = Static.SortByAllowedColumns[0],
                                PageSize = PageSize,
                                PageNumber = Static.MimimumPageNumber + 1,
                                SortDirection = GymAndYou.Models.Query_Models.SortDirection.Desc
                            }
                        }
                  );


            });

            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
