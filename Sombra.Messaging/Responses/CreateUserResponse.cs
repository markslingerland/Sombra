using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class CreateUserResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}