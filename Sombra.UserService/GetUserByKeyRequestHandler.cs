using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class GetUserByKeyRequestHandler : IAsyncRequestHandler<GetUserByKeyRequest, GetUserByKeyResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public GetUserByKeyRequestHandler(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserByKeyResponse> Handle(GetUserByKeyRequest message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            return user != null ? _mapper.Map<GetUserByKeyResponse>(user) : new GetUserByKeyResponse();
        }
    }
}