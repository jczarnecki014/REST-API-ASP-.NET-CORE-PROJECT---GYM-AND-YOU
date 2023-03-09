using AutoMapper;
using GymAndYou.DatabaseConnection;
using GymAndYou.Entities;
using GymAndYou.Exceptions;
using GymAndYou.Models.DTO_Models;
using GymAndYou.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymAndYou.Services
{
    public class AccountService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly DbConnection _db;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(DbConnection db, ILogger<AccountService> logger, IMapper mapper,IPasswordHasher<User> passwordHasher,AuthenticationSettings authenticationSettings)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GetJWTToken(LoginUserDTO loginUserDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == loginUserDTO.UserName);

            if(user is null)
            {
                throw new BadRequest("User with that userName doesn't exist");
            }

            var resoult = _passwordHasher.VerifyHashedPassword(user,user.PasswordHash, loginUserDTO.Password);

            if(resoult == PasswordVerificationResult.Failed)
            {
                throw new BadRequest("Invalid username or password");
            }


            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName.ToString()),
                new Claim("Nationality",user.Nationality)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken
            (
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials:cred
            
            );
            
            var TokenHandler = new JwtSecurityTokenHandler();
            return TokenHandler.WriteToken(token);

        }

        public void CreateAccount(CreateUserDTO createUserDTO)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Email == createUserDTO.Email);

            if(existingUser is not null || existingUser != null) 
            {
                throw new UserAlreadyExist("User with that email already exist");
            }

            var user = _mapper.Map<User>(createUserDTO);

            user.PasswordHash = _passwordHasher.HashPassword(user,createUserDTO.Password);

            _db.Users.Add(user);
            _db.SaveChanges();

            _logger.LogInformation($"User with email = [{user.Email}] userName = [{user.UserName}] and ID = [{user.Id}] has been created");
        }
    }
}
