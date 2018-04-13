using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UpdateUserRequestHandler : IAsyncRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserContext _context;

        public UpdateUserRequestHandler(UserContext context)
        {
            _context = context;
        }

        public Task<UpdateUserResponse> Handle(UpdateUserRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}