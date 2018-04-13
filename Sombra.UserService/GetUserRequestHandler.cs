using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class GetUserRequestHandler : IAsyncRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly UserContext _context;

        public GetUserRequestHandler(UserContext context)
        {
            _context = context;
        }

        public Task<GetUserResponse> Handle(GetUserRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}