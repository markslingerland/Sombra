
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
<<<<<<< HEAD
    public class UserLoginRequest : Request<UserLoginResponse>{
        public string LoginTypeCode { get; set; }
=======
    public class UserLoginRequest : IRequest<UserLoginResponse>{
        public Core.Enums.CredentialType LoginTypeCode { get; set; }
>>>>>>> master
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}

