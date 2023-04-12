using System.ComponentModel.DataAnnotations;

namespace GymAndYou.Entities
{

    /*
        FluentValidator ensure validation for this properties
        Balidators: path: ./Validators/AddEquipmentDTOValidator.cs
     */

    public class UpsertEquipmentDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        public string BodyPart { get;set; }

        [Required]
        public int MaxWeight { get;set; }
    }
}
