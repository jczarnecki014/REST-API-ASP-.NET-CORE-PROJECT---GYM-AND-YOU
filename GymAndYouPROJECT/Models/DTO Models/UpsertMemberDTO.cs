using System.ComponentModel.DataAnnotations;

namespace GymAndYou.DTO_Models
{
    public class UpsertMemberDTO
    {
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string Pesel { get;set; }
        public DateTime BirthDay { get;set; }
        public string Sex { get;set; }
    }
}
