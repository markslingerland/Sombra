using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        public async Task<IActionResult> Index(DonateViewModel model)
        {
            if(model.Name == null) 
            { 
                model.IsAnonymous = true;
            }
            var request = _mapper.Map<MakeDonationRequest>(model);
            if(!request.IsAnonymous){
                var userKey = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
                request.UserKey = Guid.Parse(userKey);
            }
            var response = await _bus.RequestAsync(request);
            if(response.IsRequestSuccessful){
                var viewModel = _mapper.Map<DonateResultViewModel>(response);

                return View("_ThankYou", viewModel);
            }

            HttpContext.Response.StatusCode = 400;
            return Content("Something has gone wrong");
            
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
