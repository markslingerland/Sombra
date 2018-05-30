using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class GetUserActivationTokenResponse : Response
    {
        public string ActivationToken { get; set; }
        public string UserName { get; set; }
        public ErrorType ErrorType { get; set; }
        public bool HasActivationToken => !string.IsNullOrEmpty(ActivationToken);
    }
}