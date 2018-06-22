using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests.Story;
using Sombra.Web.Infrastructure.Messaging;
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
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("verhalen/{url}")]
        public async Task<IActionResult> Detail(StoryQuery query)
        {
            var request = _mapper.Map<GetStoryByUrlRequest>(query);
            var response = await _bus.RequestAsync(request);

            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);
            if (!response.IsSuccess || !response.Story.IsApproved) return new StatusCodeResult((int)HttpStatusCode.NotFound);
            var model = _mapper.Map<StoryViewModel>(response.Story); 

            return View("Detail", model);
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
            var request = new GetRandomStoriesRequest
            {
                Amount = 1
            };
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);
            if (!response.IsSuccess || !response.Results.Any()) return new StatusCodeResult((int)HttpStatusCode.NotFound);

            var model = _mapper.Map<RandomStoryViewModel>(response.Results.First());
            return PartialView("_RandomStory", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetRelatedStories()
        {
            var request = new GetRandomStoriesRequest
            {
                Amount = 3
            };
            var response = await _bus.RequestAsync(request);
            if (!response.IsRequestSuccessful) return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);
            if (!response.IsSuccess || !response.Results.Any()) return new StatusCodeResult((int)HttpStatusCode.NotFound);

            var model = _mapper.Map<RelatedStoriesViewModel>(response);
            return PartialView("_RelatedStoriesWrapper", model);
        }

        [HttpGet]
        public IActionResult Facebook()
        {
            return View("Facebook", "Story");
        }
    }
}
