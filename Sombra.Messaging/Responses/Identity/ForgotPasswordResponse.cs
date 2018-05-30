using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class ForgotPasswordResponse : Response
    {
        public string Secret { get; set; }
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}