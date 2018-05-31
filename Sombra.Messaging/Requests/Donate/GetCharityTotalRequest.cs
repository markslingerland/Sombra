using System;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.Messaging.Requests.Donate
{
    public class GetCharityTotalRequest : Request<GetCharityTotalResponse>
    {
        public Guid CharityKey { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}