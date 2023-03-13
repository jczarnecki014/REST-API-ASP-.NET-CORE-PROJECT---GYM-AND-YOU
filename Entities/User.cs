namespace GymAndYou.Entities
{
    public class User
    {
        public int Id {get;set;}
        //[REQUIRED]
        public string Email {get;set;}
        //[REQUIRED]
        public string UserName {get;set;}
        //[REQUIRED]
        public string PasswordHash  {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Nationality {get;set;}
        public DateTime? RegisterDay { get;set;}
        public int RoleId {get;set;}
        public virtual Role Role {get;set;}
        public virtual Gym userGym { get;set;}
    }
}
