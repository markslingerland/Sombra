
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class UserLoginRequest : IRequest<UserLoginResponse>{
        public string loginTypeCode { get; set; }
        public string identifier { get; set; }
        public string secret { get; set; }
    }
}

