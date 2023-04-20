using AutoMapper;
using GymAndYou.AutorizationRules;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Exceptions;
using GymAndYou.Models.Query_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace GymAndYou.Services
{
    public interface IGymService
    {
        int CreateGym(UpsertGymDTO gymDto);
        void DeleteGym(int gymId);
        PageResoult<GymDTO> GetAll(GymQuery query);
        GymDTO GetGymById(int gymId);
        void UpdateGym(int gymId, UpsertGymDTO gymDTO);
        Gym GetGym(int gymId,string Include);
    }

    public class GymService : IGymService
    {
        private readonly DbConnection _db;
        private readonly ILogger<GymService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;
        private readonly IAuthorizationService _authorizationService;

        public GymService(DbConnection db, ILogger<GymService> logger, IMapper mapper, IUserContextService userContext,IAuthorizationService authorizationService)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _userContext = userContext;
            _authorizationService = authorizationService;
        }

        public PageResoult<GymDTO> GetAll(GymQuery query)
        {
            var baseQuery = _db.Gyms
                        .Include("Address")
                        .Include("AviableEquipments")
                        .Where(u => query.SearhPhrase == null || 
                               u.Name.ToLower().Contains(query.SearhPhrase.ToLower()) || 
                               u.Description.ToLower().Contains(query.SearhPhrase.ToLower()));
                        
            if(!String.IsNullOrEmpty(query.SortBy))
            {
                var columnSelectors = new Dictionary<string,Expression<Func<Gym,object>>>
                {
                    {nameof(Gym.Name), r => r.Name },
                    {nameof(Gym.Description), r => r.Description }
                };

                var selectedColumn = columnSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Asc ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }

            var gyms = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize)
                        .ToList();

            var gymDTO = _mapper.Map<List<GymDTO>>(gyms);

            var totalItemsCount = baseQuery.Count();

            var resoults = new PageResoult<GymDTO>(gymDTO,query.PageSize,query.PageNumber,totalItemsCount);

            return resoults;
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
            var gym = _mapper.Map<Gym>(gymDto);
            gym.CreatedById = _userContext.GetUserId;


            _db.Gyms.Add(gym);
            _db.SaveChanges();
            _logger.LogInformation($"Created new gym with id = {gym.Id}");

            return gym.Id;
        }

        public void DeleteGym(int gymId)
        {
            var gym = GetGymWithAuthorization(gymId,"Address",ResourceOperations.Delete);
           
            _db.Gyms.Remove(gym);
            _db.SaveChanges();
            _logger.LogInformation($"Deleted gym with id = {gymId}");
        }

        public void UpdateGym(int gymId, UpsertGymDTO gymDTO)
        {
                var gym = GetGymWithAuthorization(gymId,"Address",ResourceOperations.Update);

                gym.Name = gymDTO.Name;
                gym.Description = gymDTO.Description;
                gym.OpeningHours = gymDTO.OpeningHours;
                gym.Address.City = gymDTO.City;
                gym.Address.StreetName = gymDTO.StreetName;
                gym.Address.PostalCode = gymDTO.PostalCode;

                _db.SaveChanges();
                _logger.LogInformation($"Update gym with id = {gymId}");
        }

        private Gym GetGymWithAuthorization(int gymId, string include,ResourceOperations operation)
        {
            var gym = _db.Gyms
                        .Include(include)
                        .FirstOrDefault(u => u.Id == gymId);

            if (gym is null)
            {
                throw new EntityNotFound("Gym with this ID doesn't exist");
            }

            var authorizationResoult = _authorizationService.AuthorizeAsync(_userContext.user,gym,new ResourceOperationRequirement(operation)).Result;
            
            if(!authorizationResoult.Succeeded)
            {
                throw new ForbidException("You haven't permission to this gym");
            }

            return gym;
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
