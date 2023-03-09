using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Models.Query_Models;
using GymAndYou.Services;
using GymAndYou.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [Route("/api/gym")]
    [ApiController]
    public class GymController : ControllerBase
    {
        
        private readonly IGymService _service;

        public GymController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{gymId}")]
        public ActionResult<Gym> GetById([FromRoute] int gymId)
        {
            var gym = _service.GetGymById(gymId);
            return Ok(gym);
        }

        [HttpGet]
        public ActionResult<PageResoult<GymDTO>> GetAll([FromQuery] GymQuery query)
        {
            var gyms = _service.GetAll(query);
            return Ok(gyms);
        }

        [HttpPost]
        public ActionResult<string> CreateGym([FromBody] UpsertGymDTO gym)
        {
           int gymId = _service.CreateGym(gym);
           return Created($"/api/gym/{gymId}",null);
        }

        [HttpDelete]
        [Route("{gymId}")]
        [Authorize(Roles = "Admin,Manager")]
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
