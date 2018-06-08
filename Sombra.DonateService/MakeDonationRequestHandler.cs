using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService
{
    public class MakeDonationRequestHandler : IAsyncRequestHandler<MakeDonationRequest, MakeDonationResponse>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public MakeDonationRequestHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MakeDonationResponse> Handle(MakeDonationRequest message)
        {
            if (message.CharityActionKey.HasValue)
            {
                var charityAction = await _context.CharityActions.SingleOrDefaultAsync(c => c.CharityActionKey == message.CharityActionKey.Value);
                if (charityAction == null)
                {
                    return new MakeDonationResponse
                    {
                        ErrorType = ErrorType.CharityActionNotFound
                    };
                }

                var charityActionDonation = _mapper.Map<CharityActionDonation>(message);
                charityActionDonation.CharityAction = charityAction;

                if (message.UserKey.HasValue && !message.IsAnonymous)
                {
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey.Value);
                    if (user == null)
                    {
                        return new MakeDonationResponse
                        {
                            Success = false,
                            ErrorType = ErrorType.UserNotFound
                        };
                    }

                    charityActionDonation.User = user;
                }

                _context.CharityActionDonations.Add(charityActionDonation);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ExtendedConsole.Log(ex);
                    return new MakeDonationResponse();
                }
                return new MakeDonationResponse
                {
                    Success = true,
                    CoverImage = charityAction.CoverImage,
                    ThankYou = charityAction.ThankYou
                };
            }
            else
            {
                var charity = await _context.Charities.SingleOrDefaultAsync(c => c.CharityKey == message.CharityKey);
                if (charity == null)
                {
                    return new MakeDonationResponse
                    {
                        ErrorType = ErrorType.CharityNotFound
                    };
                }

                var charityDonation = _mapper.Map<CharityDonation>(message);
                charityDonation.Charity = charity;

                if (message.UserKey.HasValue && !message.IsAnonymous)
                {
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey.Value);
                    if (user == null)
                    {
                        return new MakeDonationResponse
                        {
                            Success = false,
                            ErrorType = ErrorType.UserNotFound
                        };
                    }

                    charityDonation.User = user;
                }

                _context.CharityDonations.Add(charityDonation);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ExtendedConsole.Log(ex);
                    return new MakeDonationResponse();
                }
                return new MakeDonationResponse
                {
                    Success = true,
                    CoverImage = charity.CoverImage,
                    ThankYou = charity.ThankYou
                };
            }
        }
    }
}