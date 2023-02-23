using Microsoft.AspNetCore.Mvc;

namespace GymAndYou.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
        return View();
        }
    }
}
