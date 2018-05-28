using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
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