﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class CreateCharityRequestHandler : IAsyncRequestHandler<CreateCharityRequest, CreateCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public CreateCharityRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCharityResponse> Handle(CreateCharityRequest message)
        {
            var charity = _mapper.Map<Charity>(message);
            if (charity.CharityKey == default)
            {
                return new CreateCharityResponse
                {
                    ErrorType = ErrorType.InvalidKey
                };
            }

            _context.Charities.Add(charity);

            return await _context.TrySaveChangesAsync<CreateCharityResponse>();
        }
    }
}
