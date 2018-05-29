using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.User
{
    public class UpdateUserResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}