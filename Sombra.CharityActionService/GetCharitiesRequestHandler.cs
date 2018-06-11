using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using Sombra.Messaging.Shared;

namespace Sombra.CharityActionService
{
    public class GetCharitiesRequestHandler : IAsyncRequestHandler<GetCharitiesRequest, GetCharitiesResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;

        public GetCharitiesRequestHandler(CharityActionContext context, IMapper mapper)
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