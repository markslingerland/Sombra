using Sombra.Messaging.Responses.Identity;

namespace Sombra.Messaging.Requests.Identity
{
    public class UserLoginRequest : Request<UserLoginResponse>
    {
        public Core.Enums.CredentialType LoginTypeCode { get; set; }
        public string Identifier { get; set; }
    }
}

