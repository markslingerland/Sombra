using System;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.Messaging.Requests.Donate
{
    public class GetCharityTotalRequest : Request<GetCharityTotalResponse>
    {
        public Guid CharityKey { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IncludeCharityActions { get; set; }
        public int NumberOfDonations { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}