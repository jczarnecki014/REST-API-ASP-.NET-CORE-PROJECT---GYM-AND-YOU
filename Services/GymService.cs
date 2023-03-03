using AutoMapper;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;

namespace GymAndYou.Services
{
    public interface IGymService
    {
        int CreateGym(UpsertGymDTO gymDto);
        void DeleteGym(int gymId);
        List<GymDTO> GetAll();
        GymDTO GetGymById(int gymId);
        void UpdateGym(int gymId, UpsertGymDTO gymDTO);
        Gym GetGym(int gymId,string Include);
    }

    public class GymService : IGymService
    {
        private readonly DbConnection _db;
        private readonly ILogger<GymService> _logger;
        private readonly IMapper _mapper;

        public GymService(DbConnection db, ILogger<GymService> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public List<GymDTO> GetAll()
        {
            var gym = _db.Gyms
                        .Include("Address")
                        .Include("AviableEquipments");

            var gymDTO = _mapper.Map<List<GymDTO>>(gym);

            return gymDTO;
        }

        public GymDTO GetGymById(int gymId)
        {
            var gym = _db.Gyms
                        .Include("Address")
                        .Include("AviableEquipments")
                        .FirstOrDefault(u => u.Id == gymId);

            if (gym is null)
            {
                throw new EntityNotFound("Gym with this ID doesn't exist");
            }

            var gymDTO = _mapper.Map<GymDTO>(gym);

            return gymDTO;
        }

        public int CreateGym(UpsertGymDTO gymDto)
        {
            if (gymDto is null)
            {
                throw new Exception("Recived gym entity can't be empty");
            }

            var gym = _mapper.Map<Gym>(gymDto);

            _db.Gyms.Add(gym);
            _db.SaveChanges();
            _logger.LogInformation($"Created new gym with id = {gym.Id}");

            return gym.Id;
        }

        public void DeleteGym(int gymId)
        {
            var gym = _db.Gyms.FirstOrDefault(u => u.Id == gymId);

            if (gym is null)
            {
                throw new EntityNotFound($"Entity with id = {gymId} wasn't found");
            }

            _db.Gyms.Remove(gym);
            _db.SaveChanges();
            _logger.LogInformation($"Deleted gym with id = {gymId}");
        }

        public void UpdateGym(int gymId, UpsertGymDTO gymDTO)
        {
            var gym = _db.Gyms
                        .Include("Address")
                        .FirstOrDefault(u => u.Id == gymId);


            if (gym is null)
            {
                throw new EntityNotFound($"Entity with id = {gymId} wasn't found");
            }

                gym.Name = gymDTO.Name;
                gym.Description = gymDTO.Description;
                gym.OpeningHours = gymDTO.OpeningHours;
                gym.Address.City = gymDTO.City;
                gym.Address.StreetName = gymDTO.StreetName;
                gym.Address.PostalCode = gymDTO.PostalCode;

                _db.SaveChanges();
                _logger.LogInformation($"Update gym with id = {gymId}");
        }

        public Gym GetGym(int gymId,string Include)
        {
            var gym = _db.Gyms
                        .Include(Include)
                        .FirstOrDefault(u => u.Id == gymId);

            if (gym is null)
            {
            throw new EntityNotFound("Gym with this ID doesn't exist");
            }

            return gym;
        }


    }
}
