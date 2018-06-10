using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core.Enums;
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
            if (message.CharityKey != default) filter = filter.And(c => c.Charity.CharityKey == message.CharityKey);
            if (!string.IsNullOrEmpty(message.CharityUrl)) filter = filter.And(c => c.Charity.Url.Equals(message.CharityUrl, StringComparison.OrdinalIgnoreCase));
            if (message.Category != Category.None) filter = filter.And(c => c.Category == message.Category);
            if (message.Keywords?.Any() != null) filter = filter.And(c => $"{c.Charity.Name} {c.Name} {c.Description}".ContainsAll(message.Keywords, StringComparison.OrdinalIgnoreCase));

            return new GetCharityActionsResponse
            {
                TotalNumberOfResults = _charityActionContext.CharityActions.Count(filter),
                Results = await _charityActionContext.CharityActions.Include(b => b.UserKeys).Include(b => b.Charity).Where(filter).OrderBy(t => t.Name, message.SortOrder)
                    .ProjectToPagedListAsync<Messaging.Shared.CharityAction>(message, _mapper)
            };
        }
    }
}