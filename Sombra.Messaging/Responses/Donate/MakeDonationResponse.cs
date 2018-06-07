using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Donate
{
    public class MakeDonationResponse : Response
    {
        public bool Success { get; set; }
        public string ThankYou { get; set; }
        public string Image { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}