using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Donate
{
    public class MakeDonationResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}