using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityActionsRequest : PagedRequest<GetCharityActionsResponse>
    {
        [CacheKey]
        public Guid CharityKey { get; set; }
        
        [CacheKey]
        public bool OnlyActive { get; set; }
    }
}