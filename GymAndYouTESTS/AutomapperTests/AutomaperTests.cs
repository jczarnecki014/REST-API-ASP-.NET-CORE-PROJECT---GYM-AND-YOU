using AutoMapper;
using FluentAssertions;
using GymAndYou.Automapper_Maps;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Models.DTO_Models;
using GymAndYouTESTS.AutomapperTests.Tests_Cases;

namespace GymAndYouTESTS.AutomapperTests
{
    public class AutomaperTests
    {
        private readonly IMapper _mapper;

        public AutomaperTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new GymMapps());
            });
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // arrange
               var mapperConfiguration =  _mapper.ConfigurationProvider;

            // act
                Action act = () => mapperConfiguration.AssertConfigurationIsValid();
            // assert
                act.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(Gym_On_GymDTO_TestCases))]
        public void Mapping_ForGivenGym_ReturnGymDTO(Gym gym, GymDTO gymDTO)
        {
            // act
                var result = _mapper.Map<GymDTO>(gym);

            // assert
                result.Should().BeEquivalentTo(gymDTO);
        }

        [Theory]
        [ClassData(typeof(AviableEquipment_On_AviableEquipmentDTO_TestCases))]
        public void Mapping_ForGivenAviableEquipment_ReturnAviableEquipmentDTO(AviableEquipment aviableEquipment, AviableEquipmentDTO aviableEquipmentDTO)
        {
            // act
                var result = _mapper.Map<AviableEquipmentDTO>(aviableEquipment);

            // assert
                result.Should().BeEquivalentTo(aviableEquipmentDTO);
        }

        [Theory]
        [ClassData(typeof(UpsertEquipmentDTO_On_AviableEquipment_TestCases))]
        public void Mapping_ForGivenUpsertEquipmentDTO_ReturnAviableEquipment(UpsertEquipmentDTO upsertEquipmentDTO, AviableEquipment aviableEquipment)
        {
            // act
                var result = _mapper.Map<AviableEquipment>(upsertEquipmentDTO);

            // assert
                result.Should().BeEquivalentTo(aviableEquipment);
        }

        [Theory]
        [ClassData(typeof(UpsertGymDTO_On_Gym_TestCases))]
        public void Mapping_ForGivenUpsertGymDTO_ReturnGym(UpsertGymDTO upsertGymDTO, Gym gym)
        {
            // act
                var result = _mapper.Map<Gym>(upsertGymDTO);

            // assert
                result.Should().BeEquivalentTo(gym);
        }

        [Theory]
        [ClassData(typeof(Members_On_MembersDTO_TestCases))]
        public void Mapping_ForGivenMembers_ReturnMembersDTO(Members members, MembersDTO membersDTO)
        {
            // act
                var result = _mapper.Map<MembersDTO>(members);

            // assert
                result.Should().BeEquivalentTo(membersDTO);
        }

        [Theory]
        [ClassData(typeof(UpsertMemberDTO_On_Members_TestCases))]
        public void Mapping_ForGivenUpsertMemberDTO_ReturnMembers(UpsertMemberDTO upsertMemberDTO, Members members)
        {
            // act
                var result = _mapper.Map<Members>(upsertMemberDTO);

            // assert
                result.Should().BeEquivalentTo(members);
        }

        [Theory]
        [ClassData(typeof(CreateUserDTO_On_user_TestCases))]
        public void Mapping_ForGivencreateUserDTO_ReturnUser(CreateUserDTO createUserDTO, User user)
        {
            // act
                var result = _mapper.Map<User>(createUserDTO);

            // assert
                result.Should().BeEquivalentTo(user);
        }
    }
}
