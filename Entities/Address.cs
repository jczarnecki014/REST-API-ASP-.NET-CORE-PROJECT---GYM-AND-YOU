namespace GymAndYou.Entities;

    public class Address
    {
        public int Id { get; set; }
        public string City { get;set; }
        public string StreetName{ get;set; }
        public string PostalCode { get;set; }
        public int GymId { get;set; }
        public Gym Gym { get;set; }
    }

