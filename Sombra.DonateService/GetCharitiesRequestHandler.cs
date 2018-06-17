using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;
using Sombra.Messaging.Shared;

namespace Sombra.DonateService
{
    public class GetCharitiesRequestHandler : IAsyncRequestHandler<GetCharitiesRequest, GetCharitiesResponse>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public GetCharitiesRequestHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharitiesResponse> Handle(GetCharitiesRequest message)
        {
            return new GetCharitiesResponse
            {
                Charities = await _context.Charities.OrderBy(c => c.Name).ProjectToListAsync<Messaging.Responses.Donate.Charity>(_mapper)
            };
        }
    }
}