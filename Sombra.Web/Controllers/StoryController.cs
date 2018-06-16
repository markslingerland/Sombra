using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Story;
using Sombra.Web.Infrastructure;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels;
using Sombra.Web.ViewModels.Story;

namespace Sombra.Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public StoryController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet("verhalen")]
        public IActionResult Index(SubdomainViewModel query)
        {
            return View("Index", query);
        }

        [HttpGet]
        [Subdomain]
        [Route("verhalen/{url}")]
        public IActionResult Detail(StoryQuery query)
        {
            return View("Detail");
        }

        [HttpGet]
        public async Task<IActionResult> GetStories(SearchQuery query)
        {
            var request = _mapper.Map<GetStoriesRequest>(query);
            var response = await _bus.RequestAsync(request);
            if (response.IsRequestSuccessful)
            {
                var model = _mapper.Map<SearchResultsViewModel>(response);
                model.PageNumber = query.PageNumber;
                model.PageSize = query.PageSize;

                return View("_SearchResultsWrapper", model);
            }

            return View("_SearchResultsWrapper", new SearchResultsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetStory()
        {
            var request = new GetStoriesRequest
            {
                OnlyApproved = true,
                PageNumber = 1,
                PageSize = 1
            };
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

            var model = _mapper.Map<RandomStoryViewModel>(response.Results.First());
            return PartialView("_RandomStory", model);
        }
    }
}
