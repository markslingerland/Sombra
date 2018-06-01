using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels.Charity;
using Sombra.Web.ViewModels.Home;

namespace Sombra.Web.Controllers
{
    public class CharityController : Controller
    {
        public const string SubdomainParameter = "Subdomain";
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public CharityController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CharityQuery query)
        {
            var request = _mapper.Map<GetCharityByUrlRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (!response.Success) return new StatusCodeResult((int) HttpStatusCode.NotFound);

            var model = _mapper.Map<CharityViewModel>(response.Charity);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityActions(CharityActionsByCharityQuery query)
        {
            var request = _mapper.Map<GetCharityActionsRequest>(query);
            var response = await _bus.RequestAsync(request);
            var model = _mapper.Map<CharityActionsViewModel>(response);

            return PartialView("~/Views/Home/_CharityActionItemsWrapper.cshtml", model);
        }
    }
}