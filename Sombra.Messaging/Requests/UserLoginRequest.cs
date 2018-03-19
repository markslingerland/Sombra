
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class UserLoginRequest : IRequest<UserLoginResponse>{
        public string Username { get; set; }
	    public string Password { get; set; }
    }
}

