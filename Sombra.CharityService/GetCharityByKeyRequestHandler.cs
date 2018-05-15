using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using Sombra.Core;

namespace Sombra.CharityService
{
    public class GetCharityByKeyRequestHandler : IAsyncRequestHandler<GetCharityRequest, GetCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharityByKeyRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharityResponse> Handle(GetCharityRequest message)
        {
            ExtendedConsole.Log("GetCharityByKeyRequestHandler received");
            var charity = await _context.Charities.Where(b => b.CharityKey.Equals(message.CharityKey)).Select(a => a).FirstOrDefaultAsync();
            return charity != null ? _mapper.Map<GetCharityResponse>(charity) : new GetCharityResponse();
        }
    }
}