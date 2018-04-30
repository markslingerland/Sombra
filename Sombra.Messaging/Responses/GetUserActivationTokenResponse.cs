using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class GetUserActivationTokenResponse : Response
    {
        public string ActivationToken { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}