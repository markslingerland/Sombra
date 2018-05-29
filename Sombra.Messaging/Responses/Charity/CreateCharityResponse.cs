using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Charity
{
    public class CreateCharityResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}