using System;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityByKeyRequest : Request<GetCharityByKeyResponse>
    {
        [CacheKey]
        public Guid CharityKey { get; set; }
    }
}