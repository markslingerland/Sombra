using System;
using Sombra.Messaging.Responses.Donate;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Requests.Donate
{
    public class MakeDonationRequest : Request<MakeDonationResponse>
    {
        public Guid? UserKey { get; set; }
        public bool IsAnonymous { get; set; }
        public DonationType DonationType { get; set; }
        public decimal Amount { get; set; }
        public Guid CharityKey { get; set; }
        public Guid? CharityActionKey { get; set; }
    }
}