using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    [Cachable]
    public class GetCharityActionsRequest : Request<GetCharityActionsResponse>
    {
        [CacheKey]
        public Guid CharityKey { get; set; }

        [CacheKey]
        public int PageSize { get; set; }

        [CacheKey]
        public int PageNumber { get; set; }
    }
}