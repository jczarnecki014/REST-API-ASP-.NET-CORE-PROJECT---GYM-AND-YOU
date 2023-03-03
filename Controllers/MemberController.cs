using GymAndYou.DTO_Models;
using GymAndYou.Entities;
using GymAndYou.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [ApiController]
    [Route("/api/gym/{gymId}/members/")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Members>> GetAll([FromRoute] int gymId)
        {
            var members = _service.GetAll(gymId);
            return Ok(members);
        }

        [HttpGet("{memberId}")]
        public ActionResult<Members> GetById([FromRoute] int gymId, [FromRoute] int memberId)
        {
            var member = _service.GetById(gymId,memberId);
            return Ok(member);
        }

        [HttpPost]
        public ActionResult<string> CreateMember([FromRoute] int gymId, [FromBody] UpsertMemberDTO memberDTO)
        {
            var memberId = _service.CreateMember(gymId,memberDTO);
            return Created($"/api/gym/{gymId}/members/{memberId}",null);
        }

        [HttpDelete("{memberId}")]
        public IActionResult DeleteMember([FromRoute] int gymId, [FromRoute] int memberId)
        {
            _service.DeleteMember(gymId, memberId);
            return NoContent();
        }

        [HttpPut("{memberId}")]
        public IActionResult UpdateMember([FromRoute] int gymId, [FromRoute] int memberId, [FromBody] UpsertMemberDTO upsertMemberDTO)
        {
           _service.UpdateMember(gymId,memberId, upsertMemberDTO);
           return NoContent();
        }

    }
}
