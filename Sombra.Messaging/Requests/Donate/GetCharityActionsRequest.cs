using System;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.Messaging.Requests.Donate
{
    public class GetCharityActionsRequest : Request<GetCharityActionsResponse>
    {
        public Guid CharityKey { get; set; }
    }
}