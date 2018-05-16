using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Sombra.Core;

namespace Sombra.CharityService
{
    public class GetCharityByKeyRequestHandler : IAsyncRequestHandler<GetCharityByKeyRequest, GetCharityByKeyResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharityByKeyRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharityByKeyResponse> Handle(GetCharityByKeyRequest message)
        {
            ExtendedConsole.Log("GetCharityByKeyRequestHandler received");
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.CharityKey.Equals(message.CharityKey));
            return charity != null ? _mapper.Map<GetCharityByKeyResponse>(charity) : new GetCharityByKeyResponse();
        }
    }
}