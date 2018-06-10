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
    public class GetCharityActionsRequestHandler : IAsyncRequestHandler<GetCharityActionsRequest, GetCharityActionsResponse>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public GetCharityActionsRequestHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharityActionsResponse> Handle(GetCharityActionsRequest message)
        {
            return new GetCharityActionsResponse
            {
                CharityActions = await _context.CharityActions.Where(c => c.Charity.CharityKey == message.CharityKey).OrderBy(c => c.Name).ProjectToListAsync<KeyNamePair>(_mapper)
            };
        }
    }
}