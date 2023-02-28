using AutoMapper;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GymAndYou.Services
{
    public class EquipmentService
    {
        private readonly DbConnection _db;
        private readonly ILogger<EquipmentService> _logger;
        private readonly IMapper _mapper;
        public EquipmentService(DbConnection db,ILogger<EquipmentService> logger,IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public List<AviableEquipmentDTO> GetAll(int gymId)
        {
            var gym = GetGym(gymId);

            var aviableEquipment = _mapper.Map<List<AviableEquipmentDTO>>(gym.AviableEquipments);

            return aviableEquipment;
        }

        public AviableEquipmentDTO GetById(int gymId, int equipmentId)
        {
            var gym = GetGym(gymId);

            var equipment = _db.AviableEquipments.FirstOrDefault(u => u.Id == equipmentId);

            if(equipment is null || equipment.GymId != gymId)
            {
                throw new EntityNotFound("Equipment or Gym with this Id doesn't exist");
            }

            var aviableEquipment = _mapper.Map<AviableEquipmentDTO>(equipment);

            return aviableEquipment;
        }

        public int AddEquipment(int gymId, AddEquipmentDTO equipmentDTO)
        {
            var gym = GetGym(gymId);

            var Equipment = _mapper.Map<AviableEquipment>(equipmentDTO);
            Equipment.GymId = gymId;

            _db.AviableEquipments.Add(Equipment);
            _db.SaveChanges();

            _logger.LogInformation($"Equipment with ID = {Equipment.Id} has been added to gym with ID = {gymId} ");

            return Equipment.Id;

        }

        public void DeleteEquipment(int equipmentId)
        {
            var equipment = _db.AviableEquipments.FirstOrDefault( u => u.Id == equipmentId);

            if(equipment is null)
            {
                throw new EntityNotFound("Equipment with this ID doesn't exist");
            }

            _db.AviableEquipments.Remove(equipment);
            _db.SaveChanges();

            _logger.LogInformation($"Equipment with ID = {equipmentId} has been removed from gym with ID = {equipment.GymId} ");
        }

        private Gym GetGym(int gymId)
        {
            var gym = _db.Gyms
                        .Include("AviableEquipments")
                        .FirstOrDefault(u => u.Id == gymId);

            if(gym is null)
            {
                throw new EntityNotFound("Gym with this ID doesn't exist");
            }

            return gym;
        }

    }
}
