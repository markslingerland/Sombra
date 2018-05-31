using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByKeyRequestHandler : IAsyncRequestHandler<GetCharityActionByKeyRequest, GetCharityActionByKeyResponse>
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionByKeyRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionByKeyResponse> Handle(GetCharityActionByKeyRequest message)
        {
            var charityAction = await _charityActionContext.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));
            if (charityAction != null)
            {
                var response = _mapper.Map<GetCharityActionByKeyResponse>(charityAction);
                
                return response;
            }
            return new GetCharityActionByKeyResponse();
        }
    }
}