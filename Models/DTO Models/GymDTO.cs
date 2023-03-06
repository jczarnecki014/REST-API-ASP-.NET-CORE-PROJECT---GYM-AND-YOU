using System.ComponentModel.DataAnnotations;

namespace GymAndYou.DTO_Models
{
    public class GymDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set;}
        public string City { get;set; }
        public string StreetName{ get;set; }
        public string PostalCode { get;set; }
        public List<AviableEquipmentDTO> AviableEquipments { get; set; }
    }
}
