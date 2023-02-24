namespace GymAndYou.Entities;

    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set;}
        public int AddressId { get;set;}
        public Address Address{ get; set; }
        public List<AviableEquipment>? AviableEquipments{ get;set;}
        public List<Members>? Members{ get;set;}
    }
