
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class UserLoginRequest : IRequest<UserLoginResponse>{
        public string LoginTypeCode {
            get { return LoginTypeCode; }
            set { LoginTypeCode = value.ToLower(); }
        }
        public string Identifier { 
            get { return Identifier; }
            set { Identifier = value.ToLower(); }
         }
        public string Secret { get; set; }
    }
}

