using Sombra.Messaging.Responses.Donate;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Requests.Donate
{
    public class MakeDonationRequest : Request<MakeDonationResponse>
    {
        public bool IsAnonymous { get; set; }
        public DonationType DonationType { get; set; }
        public decimal Amount { get; set; }

    }
}