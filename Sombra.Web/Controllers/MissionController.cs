using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class MissionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}