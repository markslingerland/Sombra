using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Story;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels.Dashboard;

namespace Sombra.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public DashboardController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("goed-doel-aanmelden")]
        public IActionResult SignUpCharity()
        {
            return View();
        }

        [HttpGet("deel-jouw-verhaal")]
        public IActionResult CreateStory()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCharities()
        {
            var request = new GetCharitiesRequest();
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var charities = _mapper.Map<List<Charity>>(response.Charities);
            return Json(charities);
        }
    }
}