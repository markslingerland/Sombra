using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class ChartController : Controller
    {
        public ChartController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}