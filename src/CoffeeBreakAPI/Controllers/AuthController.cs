using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreakAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
