using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class UserLoginRequest : IRequest<UserLoginResponse>
    {
        public Core.Enums.CredentialType LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}

