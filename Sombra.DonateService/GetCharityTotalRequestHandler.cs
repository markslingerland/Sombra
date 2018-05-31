using System;
using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService
{
    public class GetCharityTotalRequestHandler : IAsyncRequestHandler<GetCharityTotalRequest, GetCharityTotalResponse>
    {
        private readonly DonationsContext _context;

        public GetCharityTotalRequestHandler(DonationsContext context)
        {
            _context = context;
        }

        public Task<GetCharityTotalResponse> Handle(GetCharityTotalRequest message)
        {
            throw new NotImplementedException();
        }
    }
}