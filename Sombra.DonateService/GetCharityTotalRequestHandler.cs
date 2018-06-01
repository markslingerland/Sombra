using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sombra.Core.Extensions;
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

        public async Task<GetCharityTotalResponse> Handle(GetCharityTotalRequest message)
        {
            Expression<Func<CharityDonation, bool>> filter = c => true;

            if (message.CharityKey != default) filter = filter.And(l => l.Charity.CharityKey == message.CharityKey);
            if (message.To.HasValue) filter = filter.And(l => l.DateTimeStamp <= message.To);
            if (message.From.HasValue) filter = filter.And(l => l.DateTimeStamp >= message.From);

            var charityDonations = _context.CharityDonations.Where(filter);            
            if (message.IncludeCharityActions)
            {
                var charityActionDonations = _context.ChartyActionDonations.Where(c => c.CharityAction.Charity.CharityKey == message.CharityKey);
                return new GetCharityTotalResponse()
                {
                    TotalDonatedAmount = charityDonations.Sum(d => d.Amount) + charityActionDonations.Sum(d => d.Amount),
                    LastDonation = charityDonations.OrderByDescending(c => c.DateTimeStamp).First().Amount,
                    NumberOfDonators = charityDonations.Count() + charityActionDonations.Count(),
                };
            }

            return new GetCharityTotalResponse()
            {
                TotalDonatedAmount = charityDonations.Sum(d => d.Amount),
                LastDonation = charityDonations.OrderByDescending(c => c.DateTimeStamp).First().Amount,
                NumberOfDonators = charityDonations.Count(),
            };
        }
    }
}