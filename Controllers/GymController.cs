using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [Route("/api/gym")]
    [ApiController]
    public class GymController : ControllerBase
    {
        
        private readonly GymService _service;

        public GymController(GymService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{gymId}")]
        public IActionResult GetById([FromRoute] int gymId)
        {
            var gym = _service.GetGymById(gymId);
            return Ok(gym);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var gyms = _service.GetAll();
            return Ok(gyms);
        }

        [HttpPost]
        public IActionResult CreateGym([FromBody] UpsertGymDTO gym)
        {
           int gymId = _service.CreateGym(gym);
           return Created($"/api/gym/{gymId}",null);
        }

        [HttpDelete]
        [Route("{gymId}")]
        public IActionResult DeleteGym([FromRoute] int gymId)
        {
            _service.DeleteGym(gymId);
            return NoContent();
        }

        [HttpPut]
        [Route("{gymId}")]
        public IActionResult UpdateGym([FromRoute] int gymId, [FromBody] UpsertGymDTO gymDTO)
        {
            _service.UpdateGym(gymId,gymDTO);
            return Ok();
        }
    }
}
