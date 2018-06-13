using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Requests.Story;
using Sombra.Web.Infrastructure;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels.Charity;
using Sombra.Web.ViewModels.Shared;
using GetCharityActionsRequest = Sombra.Messaging.Requests.CharityAction.GetCharityActionsRequest;

namespace Sombra.Web.Controllers
{
    public class CharityController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public CharityController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet("goede-doelen")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        [Subdomain]
        public async Task<IActionResult> Index(CharityQuery query)
        {
            var request = _mapper.Map<GetCharityByUrlRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.IsSuccess || !response.Charity.IsApproved) return new StatusCodeResult((int) HttpStatusCode.NotFound);

            var model = _mapper.Map<CharityViewModel>(response.Charity);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityActions(CharityActionsByCharityQuery query)
        {
            var request = _mapper.Map<GetCharityActionsRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var model = _mapper.Map<CharityActionsViewModel>(response);
            return PartialView("_CharityActionItemsWrapper", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityDonations(DonationsInWeekByCharityQuery query)
        {
            var request = _mapper.Map<GetCharityTotalRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var model = _mapper.Map<DonationsViewModel>(response);
            return PartialView("~/Views/Charity/_DonationItemsWrapper.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityStory(CharityStoryQuery query)
        {
            var request = _mapper.Map<GetStoriesRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var model = _mapper.Map<StoryViewModel>(response.Results.First());
            return PartialView("_Story", model);
        }
    }
}