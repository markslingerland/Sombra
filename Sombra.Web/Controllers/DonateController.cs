using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Donate;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels.Donate;

namespace Sombra.Web.Controllers
{
    public class DonateController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public DonateController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet("doneren")]
        public IActionResult Index()
        {
            return View(new DonateViewModel());
        }

        [HttpPost("doneren")]
        public IActionResult Index(DonateViewModel model)
        {
            return View(new DonateViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetCharities()
        {
            var request = new GetCharitiesRequest();
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int) HttpStatusCode.ServiceUnavailable);

            var charities = _mapper.Map<List<Charity>>(response.Charities);
            return Json(charities);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityActions(string charityKey)
        {
            var charityKeyIsValidGuid = Guid.TryParse(charityKey, out var charityKeyGuid);
            if (!charityKeyIsValidGuid) return new StatusCodeResult((int) HttpStatusCode.BadRequest);

            var request = new GetCharityActionsRequest
            {
                CharityKey = charityKeyGuid
            };
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int) HttpStatusCode.ServiceUnavailable);

            var charityActions = _mapper.Map<List<CharityAction>>(response.CharityActions);
            return Json(charityActions);
        }
    }
}
