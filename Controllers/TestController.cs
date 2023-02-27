using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    [Route("/api")]
    public class TestController : ControllerBase
    {
        public IActionResult Index()
        {
            throw (new Exception());
            return Ok("rlo");
        }
    }
}
