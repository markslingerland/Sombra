using System.Threading.Tasks;
using AutoMapper;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class ApproveStoryRequestHandler : IAsyncRequestHandler<ApproveStoryRequest, ApproveStoryResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public ApproveStoryRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ApproveStoryResponse> Handle(ApproveStoryRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}