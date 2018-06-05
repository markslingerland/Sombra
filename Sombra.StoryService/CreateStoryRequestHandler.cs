using System.Threading.Tasks;
using AutoMapper;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CreateStoryRequestHandler : IAsyncRequestHandler<CreateStoryRequest, CreateStoryResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public CreateStoryRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<CreateStoryResponse> Handle(CreateStoryRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}