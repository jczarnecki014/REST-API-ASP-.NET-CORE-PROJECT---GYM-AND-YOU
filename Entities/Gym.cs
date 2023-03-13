using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GymAndYou.Entities;

    public class Gym
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set;}
        public int? CreatedById { get; set; }
        public User? CreatedBy{ get; set; }
        public Address Address{ get; set; }
        public List<AviableEquipment>? AviableEquipments{ get;set;}
        public List<Members>? Members{ get;set;}
    }
