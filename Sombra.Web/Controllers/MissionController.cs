using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class MissionController : Controller
    {
        [HttpGet("onze-missie")]
        public IActionResult Index()
        {
            return View();
        }
    }
}