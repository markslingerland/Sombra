using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class CharityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}