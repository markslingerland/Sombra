using Sombra.Messaging.Responses.User;

namespace Sombra.Messaging.Requests.User
{
    public class GetUserByEmailRequest : Request<GetUserByEmailResponse>
    {
        public string EmailAddress { get; set; }
    }
}