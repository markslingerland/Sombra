using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class ChangePasswordResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}