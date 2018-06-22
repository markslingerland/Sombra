using System;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.Messaging.Requests.Donate
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityActionsRequest : Request<GetCharityActionsResponse>
    {
        [CacheKey]
        public Guid CharityKey { get; set; }
    }
}