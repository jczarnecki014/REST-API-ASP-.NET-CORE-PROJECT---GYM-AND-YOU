using System.ComponentModel.DataAnnotations;

namespace GymAndYou.DTO_Models
{
    public class UpsertMemberDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        //[CUSTOM VALIDATION]
        public string Pesel { get;set; }
        public DateTime BirthDay { get;set; }
        //[CUSTOM VALIDATION]
        public string Sex { get;set; }
    }
}
