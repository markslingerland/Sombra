using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.Controllers
{
    public class CreateStoryController : Controller
    {
        public const string SubdomainParameter = "Subdomain";
        public IActionResult Index()
        {
            return View();
        }
    }
}
