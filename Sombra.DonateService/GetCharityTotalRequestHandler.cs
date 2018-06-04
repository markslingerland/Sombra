using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Extensions;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService
{
    public class GetCharityTotalRequestHandler : IAsyncRequestHandler<GetCharityTotalRequest, GetCharityTotalResponse>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public GetCharityTotalRequestHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                var charityActionDonations = _context.CharityActionDonations.Where(c => c.CharityAction.Charity.CharityKey == message.CharityKey);
                return new GetCharityTotalResponse
                {
                    TotalDonatedAmount = charityDonations.Sum(d => d.Amount) + charityActionDonations.Sum(d => d.Amount),
                    NumberOfDonators = charityDonations.Count() + charityActionDonations.Count(),
                    Donations = await _context.CharityDonations.Include(c => c.User).Where(filter).OrderBy(d => d.DateTimeStamp, message.SortOrder).Take(message.NumberOfDonations).ProjectToListAsync<Donation>(_mapper)
                };
            }

            return new GetCharityTotalResponse
            {
                TotalDonatedAmount = charityDonations.Sum(d => d.Amount),
                NumberOfDonators = charityDonations.Count(),
                Donations = await _context.CharityDonations.Include(c => c.User).Where(filter).OrderBy(d => d.DateTimeStamp, message.SortOrder).Take(message.NumberOfDonations).ProjectToListAsync<Donation>(_mapper)
            };
        }
    }
}