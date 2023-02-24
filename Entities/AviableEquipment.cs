namespace GymAndYou.Entities;

    public class AviableEquipment
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string BodyPart { get;set; }
        public int MaxWeight { get;set; }
        public int GymId { get; set; }
        public Gym Gym { get;set; }

    }

