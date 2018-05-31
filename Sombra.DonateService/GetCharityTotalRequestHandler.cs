using System;
using System.Linq;
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

        public async Task<GetCharityTotalResponse> HandleAsync(GetCharityTotalRequest message)
        {
            var charityActionDonations = _context.ChartyActionDonations.Where(c => c.CharityAction.Charity.CharityKey == message.CharityKey);

            return new GetCharityTotalResponse(){
                TotalDonatedAmount = charityActionDonations.Sum(d => d.Amount),
                LastDonation = charityActionDonations.OrderByDescending(c => c.DateTimeStamp).First().Amount,
                NumberOfDonators = charityActionDonations.Count(),
            };
        }
    }
}