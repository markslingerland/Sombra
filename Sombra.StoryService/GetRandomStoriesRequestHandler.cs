using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class GetRandomStoriesRequestHandler : IAsyncRequestHandler<GetRandomStoriesRequest, GetRandomStoriesResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetRandomStoriesRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetRandomStoriesResponse> Handle(GetRandomStoriesRequest message)
        {
            return new GetRandomStoriesResponse
            {
                IsSuccess = true,
                Results = await _context.Stories.Where(c => c.IsApproved)
                    .OrderBy(c => Guid.NewGuid()).Take(message.Amount)
                    .ProjectToListAsync<Messaging.Shared.Story>(_mapper)
            };
        }
    }
}