using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [Route("/api/gym/{gymId}/equipment/")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentService _service;
        public EquipmentController(EquipmentService equipmentService)
        {
            _service = equipmentService;
        }

        [HttpGet]
        public IActionResult GetAll([FromRoute] int gymId)
        {
            var AviableEquipmentDTO = _service.GetAll(gymId);
            return Ok(AviableEquipmentDTO);
        }

        [HttpGet("{equipmentId}")]
        public IActionResult Get([FromRoute] int gymId,[FromRoute] int equipmentId)
        {
            var AviableEquipmentDTO = _service.GetById(gymId,equipmentId);
            return Ok(AviableEquipmentDTO);
        }

        [HttpPost]
        public IActionResult CreateEquipment([FromRoute] int gymId, [FromBody] AddEquipmentDTO equipmentDTO)
        {
            int equipmentId = _service.AddEquipment(gymId,equipmentDTO);
            return Created($"/api/gym/{gymId}/equipment/{equipmentId}",null);
        }

        [HttpDelete("{equipmentId}")]
        public IActionResult RemoveEquipment([FromRoute] int equipmentId)
        {
            _service.DeleteEquipment(equipmentId);
            return NoContent();
        }

    }
}
