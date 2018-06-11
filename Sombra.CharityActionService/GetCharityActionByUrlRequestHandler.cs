using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByUrlRequestHandler : IAsyncRequestHandler<GetCharityActionByUrlRequest, GetCharityActionByUrlResponse>
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionByUrlRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionByUrlResponse> Handle(GetCharityActionByUrlRequest message)
        {
            var charityAction = await _charityActionContext.CharityActions.Include(b => b.UserKeys).Include(b => b.Charity)
                .FirstOrDefaultAsync(b => b.UrlComponent.Equals(message.CharityActionUrlComponent, StringComparison.OrdinalIgnoreCase) && b.Charity.Url.Equals(message.CharityUrl, StringComparison.OrdinalIgnoreCase));

            return charityAction != null ? _mapper.Map<GetCharityActionByUrlResponse>(charityAction) : new GetCharityActionByUrlResponse();
        }
    } 
}
