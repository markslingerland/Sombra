
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class UserLoginRequest : Request<UserLoginResponse>{
        public string LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}

