using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class UserEmailExistsRequest : Request<UserEmailExistsResponse>
    {
        public string EmailAddress { get; set; }
    }
}