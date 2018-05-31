using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class CharityActionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}