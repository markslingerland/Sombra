using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class GetUserRequestHandler : IAsyncRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public GetUserRequestHandler(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            return user != null ? _mapper.Map<GetUserResponse>(user) : new GetUserResponse();
        }
    }
}