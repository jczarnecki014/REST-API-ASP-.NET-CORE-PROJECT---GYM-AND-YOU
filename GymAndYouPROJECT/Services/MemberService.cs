using AutoMapper;
using AutoMapper.Execution;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;

namespace GymAndYou.Services
{
    public interface IMemberService
    {
        int CreateMember(int gymId, UpsertMemberDTO memberDTO);
        void DeleteMember(int gymId, int memberId);
        List<MembersDTO> GetAll(int gymId);
        MembersDTO GetById(int gymId, int memberId);
        void UpdateMember(int gymId, int memberId, UpsertMemberDTO upsertMemberDTO);
    }

    public class MemberService : IMemberService
    {
        private readonly DbConnection _db;
        private readonly ILogger<MemberService> _logger;
        private readonly IMapper _mapper;
        private readonly IGymService _gymService;

        public MemberService(DbConnection db, ILogger<MemberService> logger, IMapper mapper, IGymService gymService)
        {
        _db = db;
        _logger = logger;
        _mapper = mapper;
        _gymService = gymService;
        }

        public List<MembersDTO> GetAll(int gymId)
        {
        var gym = _gymService.GetGym(gymId, "Members");

        var membersDTO = _mapper.Map<List<MembersDTO>>(gym.Members);
        return membersDTO;
        }

        public MembersDTO GetById(int gymId, int memberId)
        {
        var member = GetMember(gymId, memberId);

        var memberDTO = _mapper.Map<MembersDTO>(member);

        return memberDTO;
        }

        public int CreateMember(int gymId, UpsertMemberDTO memberDTO)
        {
        var gym = _gymService.GetGym(gymId, "Members");

        var member = _mapper.Map<Members>(memberDTO);

        member.GymId = gymId;
        member.JoinDate = DateTime.Now;

        _db.Members.Add(member);
        _db.SaveChanges();

        _logger.LogInformation($"New member with ID = {member.Id} was added to gym with ID = {gymId}");

        return member.Id;

        }

        public void DeleteMember(int gymId, int memberId)
        {
        var member = GetMember(gymId, memberId);

        _db.Members.Remove(member);
        _db.SaveChanges();

        _logger.LogInformation($"Member with ID = {memberId} was deleted");
        }

        public void UpdateMember(int gymId, int memberId, UpsertMemberDTO upsertMemberDTO)
        {
        var member = GetMember(gymId, memberId);

        member.FirstName = upsertMemberDTO.FirstName;
        member.LastName = upsertMemberDTO.LastName;
        member.Email = upsertMemberDTO.Email;
        member.Phone = upsertMemberDTO.Phone;
        member.Pesel = upsertMemberDTO.Pesel;
        member.BirthDay = upsertMemberDTO.BirthDay;
        member.Sex = upsertMemberDTO.Sex;

        _db.SaveChanges();
        _logger.LogInformation($"Member with ID = {memberId} has been updated");
        }

        private Members GetMember(int gymId, int memberId)
        {
        var gym = _gymService.GetGym(gymId, "Members");

        var member = gym.Members.FirstOrDefault(u => u.Id == memberId);

        if (member is null)
        {
        throw new EntityNotFound("Member with that ID doesn't exist");
        }

        return member;
        }
    }
}
