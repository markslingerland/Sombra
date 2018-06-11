using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class GetStoryByUrlRequestHandler : IAsyncRequestHandler<GetStoryByUrlRequest, GetStoryByUrlResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetStoryByUrlRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetStoryByUrlResponse> Handle(GetStoryByUrlRequest message)
        {
            var story = await _context.Stories.IncludeAll().FirstOrDefaultAsync(b => b.UrlComponent.Equals(message.StoryUrlComponent, StringComparison.OrdinalIgnoreCase) && b.Charity.Url.Equals(message.CharityUrl, StringComparison.OrdinalIgnoreCase));

            return story != null ? _mapper.Map<GetStoryByUrlResponse>(story) : new GetStoryByUrlResponse();
        }
    }
}