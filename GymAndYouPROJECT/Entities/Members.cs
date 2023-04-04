using GymAndYou.Entities.EntitiesInterface;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymAndYou.Entities;
public class Members : IDbEntity 
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
        [ForeignKey(nameof(GymId))]
        public Gym Gym { get;set; }

    }

