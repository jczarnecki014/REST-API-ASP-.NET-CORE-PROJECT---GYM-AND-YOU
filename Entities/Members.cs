namespace GymAndYou.Entities;
    public class Members
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pesel { get;set; }
        public DateTime BirthDay { get;set; }
        public string Sex { get;set; }
        public DateTime JoinDate { get;set;}
        public int GymId { get;set; }
        public Gym Gym { get;set; }

    }

