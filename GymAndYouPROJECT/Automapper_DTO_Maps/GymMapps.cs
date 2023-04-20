using AutoMapper;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Models.DTO_Models;

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

           CreateMap<UpsertEquipmentDTO,AviableEquipment>()
                .ForMember(m => m.Id, c => c.Ignore())
                .ForMember(m => m.Gym, c => c.Ignore())
                .ForMember(m => m.GymId, c => c.Ignore());

           CreateMap<UpsertGymDTO,Gym>()
               .ForMember(m => m.Address, c => c.MapFrom(dto => new Address(){ City = dto.City, StreetName = dto.StreetName, PostalCode = dto.PostalCode } ))
               .ForMember(m => m.Id, c => c.Ignore())
               .ForMember(m => m.CreatedBy, c => c.Ignore())
               .ForMember(m => m.AviableEquipments, c => c.Ignore())
               .ForMember(m => m.Members, c => c.Ignore())
               .ForMember(m => m.CreatedById, c => c.Ignore());
          
           CreateMap<Members,MembersDTO>();

           CreateMap<UpsertMemberDTO,Members>()
               .ForMember(m => m.Id, c => c.Ignore())
               .ForMember(m => m.JoinDate, c => c.Ignore())
               .ForMember(m => m.GymId, c => c.Ignore())
               .ForMember(m => m.Gym, c => c.Ignore());

           CreateMap<CreateUserDTO,User>()
               .ForMember(m => m.Id, c => c.Ignore())
               .ForMember(m => m.PasswordHash, c => c.Ignore())
               .ForMember(m => m.RegisterDay, c => c.Ignore())
               .ForMember(m => m.userGym, c => c.Ignore())
               .ForMember(m => m.Role, c => c.Ignore());
        }
    }
}
