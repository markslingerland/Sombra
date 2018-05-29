using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Threading.Tasks;
using Sombra.Core.Enums;

namespace Sombra.CharityActionService
{
    public class CreateCharityActionRequestHandler : IAsyncRequestHandler<CreateCharityActionRequest, CreateCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;

        public CreateCharityActionRequestHandler(CharityActionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCharityActionResponse> Handle(CreateCharityActionRequest message)
        {
            var charityAction = _mapper.Map<CharityAction>(message);
            if (charityAction.CharityActionKey == default)
            {
                ExtendedConsole.Log("CreateCharityActionRequestHandler: CharityActionKey is empty");
                return new CreateCharityActionResponse
                {
                    ErrorType = ErrorType.InvalidKey
                };
            }

            _context.CharityActions.Add(charityAction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateCharityActionResponse();
            }

            return new CreateCharityActionResponse { Success = true };
        }
    }
}
