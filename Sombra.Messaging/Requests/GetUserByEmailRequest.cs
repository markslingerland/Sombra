using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetUserByEmailRequest : Request<GetUserByEmailResponse>
    {
        public string EmailAddress { get; set; }
    }
}