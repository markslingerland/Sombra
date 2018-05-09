using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetUserActivationTokenRequest : Request<GetUserActivationTokenResponse>
    {
        public string EmailAddress { get; set; }
    }
}