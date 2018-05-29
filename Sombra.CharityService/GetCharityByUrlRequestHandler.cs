using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.CharityService
{
    public class GetCharityByUrlRequestHandler : IAsyncRequestHandler<GetCharityByUrlRequest, GetCharityByUrlResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharityByUrlRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharityByUrlResponse> Handle(GetCharityByUrlRequest message)
        {
            ExtendedConsole.Log("GetCharityByUrlRequest received");
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.Url.Equals(message.Url, StringComparison.OrdinalIgnoreCase) && b.IsApproved);
            return charity != null ? _mapper.Map<GetCharityByUrlResponse>(charity) : new GetCharityByUrlResponse();
        }
    }
}