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
using EasyNetQ;

namespace Sombra.SearchService
{
    public class GetSearchResultRequestHandler : IAsyncRequestHandler<GetSearchResultRequest, GetSearchResultResponse>
    {
        private readonly SearchContext _context;
        private readonly IMapper _mapper;

        public GetSearchResultRequestHandler(SearchContext context) 
        {
            _context = context;
        }

        public async Task<GetSearchResultResponse> Handle(GetSearchResultRequest message)
        {
            Expression<Func<Content, bool>> filter = c => true;

            if (!message.Categories.Equals(Core.Enums.Category.None)) filter = filter.And(l => l.Category.HasAnyFlag(message.Categories));
            if (!string.IsNullOrEmpty(message.Keyword)) filter = filter.And(l => l.Name.Contains(message.Keyword));
            if (!message.SearchContentType.Equals(Core.Enums.SearchContentType.Default)) filter = filter.And(l => message.SearchContentType.Equals(l.Type));

            var response =  new GetSearchResultResponse();
            
            response.TotalResult = _context.Content.Count(filter);
            var debug = _context.Content.Where(filter);
            var debug1 = debug.OrderBy(c => c.Name, message.SortOrder);
            var debug2 = await debug1.ProjectToPagedListAsync<SearchResult>(message.PageNumber, message.PageSize, _mapper.ConfigurationProvider);


            response.Results = await _context.Content.Where(filter).OrderBy(c => c.Name, message.SortOrder).ProjectToPagedListAsync<SearchResult>(message.PageNumber, message.PageSize, _mapper.ConfigurationProvider);
            
            return response;
        }
    }
}