using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure.Authentication;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Areas.Development.Controllers
{
    [Area("Development")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationManager _authenticationManager;
        
        public AuthenticationController(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel query)
        {
            query.CredentialType = Core.Enums.CredentialType.Email;
            var result = await _authenticationManager.SignInAsync(query);

            return View("View", result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Test()
        {
            return View("Test");
        }
    }
}