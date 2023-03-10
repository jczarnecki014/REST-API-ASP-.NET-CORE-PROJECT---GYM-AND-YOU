using GymAndYou.Models.DTO_Models;
using GymAndYou.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] CreateUserDTO userDetails)
        {
            _service.CreateAccount(userDetails);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDTO loginUserDTO)
        {
            var JWTToken = _service.GetJWTToken(loginUserDTO);
            return Ok(JWTToken);
        }
    }
}
