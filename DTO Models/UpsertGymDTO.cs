using System.ComponentModel.DataAnnotations;

namespace GymAndYou.DTO_Models
{
    public class UpsertGymDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public string OpeningHours { get; set;}
        [Required]
        public string City { get;set; }
        [Required]
        public string StreetName{ get;set; }
        [Required]
        public string PostalCode { get;set; }

    }
}
