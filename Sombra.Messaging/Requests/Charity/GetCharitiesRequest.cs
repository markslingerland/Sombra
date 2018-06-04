using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    [Cachable]
    public class GetCharitiesRequest : PagedRequest<GetCharitiesResponse>
    {
        [CacheKey]
        public List<string> Keywords { get; set; }

        [CacheKey]
        public SortOrder SortOrder { get; set; }

        [CacheKey]
        public Category Category { get; set; }
    }
}