using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityActionsRequest : PagedRequest<GetCharityActionsResponse>
    {
        [CacheKey]
        public Guid CharityKey { get; set; }

        [CacheKey]
        public string CharityUrl { get; set; }

        [CacheKey]
        public List<string> Keywords { get; set; }

        [CacheKey]
        public SortOrder SortOrder { get; set; }

        [CacheKey]
        public Category Category { get; set; }
    }
}