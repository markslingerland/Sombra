using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class GetStoryByKeyRequestHandler : IAsyncRequestHandler<GetStoryByKeyRequest, GetStoryByKeyResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetStoryByKeyRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetStoryByKeyResponse> Handle(GetStoryByKeyRequest message)
        {
            var story = await _context.Stories.IncludeAuthor().IncludeImages().FirstOrDefaultAsync(b => b.StoryKey.Equals(message.StoryKey));

            return story != null ? _mapper.Map<GetStoryByKeyResponse>(story) : new GetStoryByKeyResponse();
        }
    }
}