using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sombra.Web.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
