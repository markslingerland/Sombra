using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Sombra.Core;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Models;

namespace Sombra.Web.Areas.Development.Controllers
{
    [Area("Development")]
    public class AuthenticationController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public AuthenticationController(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
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
            var response = await _bus.RequestAsync<UserLoginRequest, UserLoginResponse>(request);
            var logs = _mapper.Map<AuthenticationViewModel>(response);

            return View("View", logs);
        }
    }
}