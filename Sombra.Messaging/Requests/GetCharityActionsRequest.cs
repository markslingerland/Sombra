using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetCharityActionsRequest : Request<GetCharityActionsResponse>
    {
        public Guid CharityKey { get; set; }
    }
}