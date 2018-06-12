using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.StoryService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.Messaging.Shared;

namespace Sombra.StoryService
{
    public class GetCharitiesRequestHandler : IAsyncRequestHandler<GetCharitiesRequest, GetCharitiesResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetCharitiesRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharitiesResponse> Handle(GetCharitiesRequest message)
        {
            return new GetCharitiesResponse
            {
                Charities = await _context.Charities.OrderBy(c => c.Name).ProjectToListAsync<KeyNamePair>(_mapper)
            };
        }
    }
}