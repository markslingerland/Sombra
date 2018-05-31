using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class GetCharityActionsRequestHandler : IAsyncRequestHandler<GetCharityActionsRequest, GetCharityActionsResponse>
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionsRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionsResponse> Handle(GetCharityActionsRequest message)
        {
            if (message.CharityKey != default)
            {
                Expression<Func<CharityAction, bool>> filter = c => c.CharityKey == message.CharityKey;
                return new GetCharityActionsResponse
                {
                    TotalResult = _charityActionContext.CharityActions.Count(filter),
                    Results = await _charityActionContext.CharityActions.Where(filter)
                        .ProjectToPagedListAsync<Messaging.Shared.CharityAction>(message.PageNumber, message.PageSize,
                            _mapper)
                };
            }

            return new GetCharityActionsResponse
            {
                TotalResult = _charityActionContext.CharityActions.Count(),
                Results = await _charityActionContext.CharityActions
                    .ProjectToPagedListAsync<Messaging.Shared.CharityAction>(message.PageNumber, message.PageSize,
                        _mapper)
            };
        }
    }
}