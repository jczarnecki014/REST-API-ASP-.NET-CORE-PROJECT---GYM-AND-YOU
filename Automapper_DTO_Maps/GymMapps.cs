using AutoMapper;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;

namespace GymAndYou.Automapper_Maps
{
    public class GymMapps: Profile
    {
        public GymMapps()
        {
           CreateMap<Gym,GymDTO>()
               .ForMember(m => m.City, c => c.MapFrom(f => f.Address.City))
               .ForMember(m => m.StreetName, c => c.MapFrom(f => f.Address.StreetName))
               .ForMember(m => m.PostalCode, c => c.MapFrom(f => f.Address.PostalCode));

           CreateMap<AviableEquipment,AviableEquipmentDTO>();

           CreateMap<UpsertGymDTO,Gym>()
               .ForMember(m => m.Address, c => c.MapFrom(dto => new Address(){ City = dto.City, StreetName = dto.StreetName, PostalCode = dto.PostalCode } ));
           
           CreateMap<UpsertEquipmentDTO,AviableEquipment>();
          
           CreateMap<Members,MembersDTO>();

           CreateMap<UpsertMemberDTO,Members>();
        }
    }
}
