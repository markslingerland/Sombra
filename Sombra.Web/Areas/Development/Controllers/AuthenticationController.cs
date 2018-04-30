using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sombra.Core;
using Sombra.Messaging.Requests;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Areas.Development.Controllers
{
    [Area("Development")]
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;


        public AuthenticationController(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AuthenticationQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthenticationQuery query)
        {
            query.LoginTypeCode = Core.Enums.CredentialType.Email;
            var response = await _userManager.SignInAsync(query);

            var result = new AuthenticationViewModel()
            { 
                Success = response
            };

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