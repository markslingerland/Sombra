using System.Threading.Tasks;
using AutoMapper;
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

        public Task<GetStoryByKeyResponse> Handle(GetStoryByKeyRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}