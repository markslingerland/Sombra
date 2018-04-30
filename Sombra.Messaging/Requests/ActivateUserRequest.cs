using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ActivateUserRequest : Request<ActivateUserResponse>
    {
        public string ActivationToken { get; set; }
    }
}