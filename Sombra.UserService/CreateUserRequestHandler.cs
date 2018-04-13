using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class CreateUserRequestHandler : IAsyncRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserContext _context;

        public CreateUserRequestHandler(UserContext context)
        {
            _context = context;
        }

        public Task<CreateUserResponse> Handle(CreateUserRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}