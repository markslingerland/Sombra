using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;
using AutoMapper;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Requests;
using System.Linq;
using System.Linq.Expressions;
using System;
using Sombra.Core.Extensions;
using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService
{
    public class GetSearchResultRequestHandler : IAsyncRequestHandler<GetSearchResultRequest, GetSearchResultResponse>
    {
        private readonly SearchContext _context;
        private readonly IMapper _mapper;

        public GetSearchResultRequestHandler(SearchContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSearchResultResponse> Handle(GetSearchResultRequest message)
        {
            Expression<Func<Content, bool>> filter = c => true;

            if (!message.Categories.Equals(Core.Enums.Category.None)) filter = filter.And(l => l.Category.HasAnyFlag(message.Categories));
            if (!string.IsNullOrEmpty(message.Keyword)) filter = filter.And(l => l.Name.Contains(message.Keyword));
            if (!message.SearchContentType.Equals(Core.Enums.SearchContentType.Default)) filter = filter.And(l => message.SearchContentType.Equals(l.Type));

            return new GetSearchResultResponse
            {
                TotalResult = _context.Content.Count(filter),
                Results = await _context.Content.Where(filter).OrderBy(c => c.Name, message.SortOrder).ProjectToPagedListAsync<SearchResult>(message.PageNumber, message.PageSize, _mapper.ConfigurationProvider)
            };
        }
    }
}