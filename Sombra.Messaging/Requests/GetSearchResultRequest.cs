using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetSearchResultRequest : Request<GetSearchResultResponse>
    {
        [CacheKey]
        public string Keyword { get; set; }

        [CacheKey]
        public Core.Enums.SortOrder SortOrder { get; set; }

        [CacheKey]
        public Core.Enums.SearchContentType SearchContentType { get; set; }

        [CacheKey]
        public Core.Enums.Category Categories { get; set; }

        [CacheKey]
        public int PageSize{ get; set; }

        [CacheKey]
        public int PageNumber{ get; set; }
    }
}