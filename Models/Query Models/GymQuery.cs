using GymAndYou.Models.Query_Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymAndYou.DTO_Models
{
    public class GymQuery
    {
        public string? SearhPhrase { get; set; }
        public string SortBy { get; set; }
        public int PageSize { get;set;}
        public int PageNumber { get;set; }
        public SortDirection SortDirection { get;set; } 
    }
}
