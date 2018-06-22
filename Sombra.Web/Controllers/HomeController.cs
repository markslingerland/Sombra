using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Requests.Search;
using Sombra.Messaging.Requests.Story;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.Models;
using Sombra.Web.ViewModels.Home;
using Sombra.Web.ViewModels.Shared;

namespace Sombra.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public HomeController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTopCharities(TopCharitiesQuery query)
        {
            var request = _mapper.Map<GetRandomCharitiesRequest>(query);
            var response = await _bus.RequestAsync(request);

            var model = new TopCharitiesViewModel
            {
                RequestedAmount = query.Amount
            };

            if (response.IsRequestSuccessful)
            {
                model.Charities = _mapper.Map<List<CharityItemViewModel>>(response.Results);
                return PartialView("_CharityItemsWrapper", model);
            }

            return PartialView("_CharityItemsWrapper", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityActions(CharityActionsQuery query)
        {
            var request = _mapper.Map<GetCharityActionsRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var model = _mapper.Map<CharityActionsViewModel>(response);
            return PartialView("_CharityActionItemsWrapper", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStory()
        {
            var request = new GetRandomStoriesRequest
            {
                Amount = 1
            };
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);
            if (!response.IsSuccess || !response.Results.Any()) return new StatusCodeResult((int)HttpStatusCode.NotFound);

            var model = _mapper.Map<StoryViewModel>(response.Results.First());
            return PartialView("_Story", model);
        }

        [HttpGet("acties")]
        public IActionResult CharityActions()
        {
            return View("CharityActions");
        }

        [HttpGet("demo-inschakelen")]
        public IActionResult EnableDemo()
        {
            HttpContext.Session.SetString("demo", "true");
            return View("Index");
        }

        [HttpGet("demo-uitschakelen")]
        public IActionResult DisableDemo()
        {
            HttpContext.Session.SetString("demo", "false");
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
