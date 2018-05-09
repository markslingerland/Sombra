using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;
using AutoMapper;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Requests;
using Microsoft.EntityFrameworkCore;

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
            var content = await _context.Content.FirstOrDefaultAsync(); //u => u.EmailAddress.Equals(message.EmailAddress, StringComparison.InvariantCultureIgnoreCase));
            return content != null ? _mapper.Map<GetSearchResultResponse>(content) : new GetSearchResultResponse();
        }
    }
}