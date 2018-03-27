using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sombra.Core;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Models;
using Sombra.Web;

namespace Sombra.Web.Areas.Development.Controllers
{
    [Area("Development")]
    public class AuthenticationController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;


        public AuthenticationController(IBus bus, IMapper mapper, IUserManager userManager)
        {
            _bus = bus;
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
            var request = _mapper.Map<UserLoginRequest>(query);
            var response = await _userManager.SignInAsync(HttpContext, request);

            return View("View", response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return View("Test");
        }
    }
}