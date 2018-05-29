using Sombra.Messaging.Responses.Identity;

namespace Sombra.Messaging.Requests.Identity
{
    public class GetUserActivationTokenRequest : Request<GetUserActivationTokenResponse>
    {
        public string EmailAddress { get; set; }
    }
}