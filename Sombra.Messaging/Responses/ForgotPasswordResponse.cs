using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class ForgotPasswordResponse : Response
    {
        public string Secret { get; set; }
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}