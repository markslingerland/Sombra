using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class CharityController : Controller
    {
        public const string SubdomainParameter = "Subdomain";
        public IActionResult Index()
        {
            return View();
        }
    }
}