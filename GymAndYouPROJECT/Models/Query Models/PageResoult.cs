using GymAndYou.DTO_Models;
using Microsoft.Identity.Client;
using NLog.Filters;

namespace GymAndYou.Models.Query_Models
{
    public class PageResoult<T> where T : class
    {
        public List<T> items { get;set; }
        public int TotalPages { get;set; }
        public int ItemFrom { get;set; }
        public int ItemTo { get;set; }
        public int TotalItemsCount { get;set;}

        public PageResoult(List<T> items,int pageSize,int pageNumber, decimal totalItemsCount)
        {
            this.items = items;
            this.TotalPages = (int) Math.Ceiling(totalItemsCount / pageSize);
            this.ItemFrom = pageSize * (pageNumber-1)+1;
            this.ItemTo = ItemFrom + pageSize - 1;
            this.TotalItemsCount = (int) totalItemsCount;
            this.TotalPages = (int) Math.Ceiling(totalItemsCount / pageSize);
        }
    }
}
