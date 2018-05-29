using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Search;
using Sombra.Messaging.Responses.Search;
using Sombra.Messaging.Shared;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    public class GetRandomCharitiesRequestHandler : IAsyncRequestHandler<GetRandomCharitiesRequest, GetRandomCharitiesResponse>
    {
        private readonly SearchContext _context;
        private readonly IMapper _mapper;

        public GetRandomCharitiesRequestHandler(SearchContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetRandomCharitiesResponse> Handle(GetRandomCharitiesRequest message)
        {
            ExtendedConsole.Log("GetRandomCharitiesRequest received");
            return new GetRandomCharitiesResponse
            {
                Results = await _context.Content.Where(c => c.Type == SearchContentType.Charity)
                    .OrderBy(c => Guid.NewGuid()).Take(message.Amount)
                    .ProjectToListAsync<SearchResult>(_mapper)
            };
        }
    }
}