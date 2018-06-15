using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.CharityService.DAL;
using Sombra.Core.Enums;
using Sombra.Core.Extensions;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class GetCharitiesRequestHandler : IAsyncRequestHandler<GetCharitiesRequest, GetCharitiesResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharitiesRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharitiesResponse> Handle(GetCharitiesRequest message)
        {
            Expression<Func<Charity, bool>> filter = c => true;
            if (message.Category != Category.None) filter = filter.And(c => c.Category.HasFlag(message.Category));
            if (message.Keywords != null && message.Keywords.Any()) filter = filter.And(c => $"{c.Slogan} {c.Name} {c.Description}".ContainsAll(message.Keywords, StringComparison.OrdinalIgnoreCase));
            if (message.OnlyApproved) filter = filter.And(c => c.IsApproved);
            if (message.OnlyUnapproved) filter = filter.And(c => !c.IsApproved);

            return new GetCharitiesResponse
            {
                TotalNumberOfResults = _context.Charities.Count(filter),
                Results = await _context.Charities.Where(filter).OrderBy(t => t.Name, message.SortOrder)
                    .ProjectToPagedListAsync<Messaging.Shared.Charity>(message, _mapper)
            };
        }
    }
}