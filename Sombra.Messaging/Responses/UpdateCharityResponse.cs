using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class UpdateCharityResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}