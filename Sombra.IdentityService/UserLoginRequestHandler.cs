using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Sombra.Messaging;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.IdentityService
{
    public class UserLoginRequestHandler : IAsyncRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly AuthenticationContext _context;
        public UserLoginRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest message)
        {
            // Hier tegen je database praten
            throw new System.NotImplementedException();
        }
    }
}