using System.ComponentModel.DataAnnotations;

namespace GymAndYou.Entities
{
    public class AddEquipmentDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string BodyPart { get;set; }
        [Required]
        public int MaxWeight { get;set; }
    }
}
