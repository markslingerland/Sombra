using Sombra.Messaging.Responses.Identity;

namespace Sombra.Messaging.Requests.Identity
{
    public class ActivateUserRequest : Request<ActivateUserResponse>
    {
        public string ActivationToken { get; set; }
    }
}