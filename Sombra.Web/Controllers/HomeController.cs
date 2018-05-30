using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Requests.Search;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.Models;
using Sombra.Web.ViewModels.Home;

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
            var model = _mapper.Map<CharityActionsViewModel>(response);

            return PartialView("_CharityActionItemsWrapper", model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
