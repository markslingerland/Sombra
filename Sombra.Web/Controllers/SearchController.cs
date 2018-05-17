using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels.Search;

namespace Sombra.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public SearchController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopCharities(TopCharitiesQuery query)
        {
            var request = _mapper.Map<GetRandomCharitiesRequest>(query);
            var response = await _bus.RequestAsync(request);

            if (response.IsRequestSuccessful)
            {
                var model = new TopCharitiesViewModel
                {
                    Charities = _mapper.Map<List<CharityItemViewModel>>(response.Results)
                };

                return PartialView("_ActionItemsWrapper", model);
            }

            return new StatusCodeResult((int) HttpStatusCode.ServiceUnavailable);
        }
    }
}