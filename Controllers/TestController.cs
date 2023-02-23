using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    public class TestController : Controller
    {
        [ApiController]
        public IActionResult Index()
        {
        return View();
        }
    }
}
