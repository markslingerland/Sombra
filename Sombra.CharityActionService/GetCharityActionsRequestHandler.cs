using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Core.Extensions;
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
            Expression<Func<CharityAction, bool>> filter = c => true;
            if (message.OnlyActive) filter = filter.And(c => c.ActionEndDateTime > DateTime.UtcNow);
            if (message.CharityKey != default) filter = filter.And(c => c.CharityKey == message.CharityKey);

            return new GetCharityActionsResponse
            {
                TotalResult = _charityActionContext.CharityActions.Count(filter),
                Results = await _charityActionContext.CharityActions.Where(filter)
                    .ProjectToPagedListAsync<Messaging.Shared.CharityAction>(message, _mapper)
            };
        }
    }
}