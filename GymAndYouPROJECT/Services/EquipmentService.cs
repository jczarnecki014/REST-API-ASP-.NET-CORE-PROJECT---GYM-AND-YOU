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
    public interface IEquipmentService
    {
        int AddEquipment(int gymId, UpsertEquipmentDTO equipmentDTO);
        void DeleteEquipment(int gymId,int equipmentId);
        List<AviableEquipmentDTO> GetAll(int gymId);
        AviableEquipmentDTO GetById(int gymId, int equipmentId);
        void UpdateEquipment(int gymId, int equipmentId, UpsertEquipmentDTO upsertEquipmentDTO);
    }

    public class EquipmentService : IEquipmentService
    {
        private readonly DbConnection _db;
        private readonly ILogger<EquipmentService> _logger;
        private readonly IMapper _mapper;
        private readonly IGymService _gymService;
        public EquipmentService(DbConnection db, ILogger<EquipmentService> logger, IMapper mapper, IGymService gymService)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _gymService = gymService;
        }

        public List<AviableEquipmentDTO> GetAll(int gymId)
        {
            var gym = _gymService.GetGym(gymId,"AviableEquipments");

            var aviableEquipment = _mapper.Map<List<AviableEquipmentDTO>>(gym.AviableEquipments);

            return aviableEquipment;
        }

        public AviableEquipmentDTO GetById(int gymId, int equipmentId)
        {
            var equipment = GetEquipment(gymId, equipmentId);

            var aviableEquipment = _mapper.Map<AviableEquipmentDTO>(equipment);

            return aviableEquipment;
        }

        public int AddEquipment(int gymId, UpsertEquipmentDTO equipmentDTO)
        {
            var gym = _gymService.GetGym(gymId,"AviableEquipments");

            var Equipment = _mapper.Map<AviableEquipment>(equipmentDTO);
            Equipment.GymId = gymId;

            _db.AviableEquipments.Add(Equipment);
            _db.SaveChanges();

            _logger.LogInformation($"Equipment with ID = {Equipment.Id} has been added to gym with ID = {gymId} ");

            return Equipment.Id;

        }

        public void DeleteEquipment(int gymId,int equipmentId)
        {
            var equipment = GetEquipment(gymId,equipmentId);

            _db.AviableEquipments.Remove(equipment);
            _db.SaveChanges();

            _logger.LogInformation($"Equipment with ID = {equipmentId} has been removed from gym with ID = {equipment.GymId} ");
        }

        public void UpdateEquipment(int gymId, int equipmentId, UpsertEquipmentDTO upsertEquipmentDTO)
        {
            var equipment = GetEquipment(gymId,equipmentId);

            equipment.Name = upsertEquipmentDTO.Name;
            equipment.Description = upsertEquipmentDTO.Description;
            equipment.BodyPart = upsertEquipmentDTO.BodyPart;
            equipment.MaxWeight = upsertEquipmentDTO.MaxWeight;

            _db.SaveChanges();

            _logger.LogInformation($"Equipment with ID = {equipmentId} has been updated");
        }

        private AviableEquipment GetEquipment(int gymId, int equipmentId)
        {
            var gym = _gymService.GetGym(gymId,"AviableEquipments");

            var equipment = gym.AviableEquipments.FirstOrDefault( f => f.Id == equipmentId);
            
            if(equipment is null || equipment.GymId != gymId)
            {
                throw new EntityNotFound("Equipment with this ID doesn't exist or doesn't exist in this gym");
            }

            return equipment;
        }
    }
}
