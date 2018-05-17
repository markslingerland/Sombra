using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetSearchResultRequest : Request<GetSearchResultResponse>
    {
        public string Keyword { get; set; }
        public Core.Enums.SortOrder SortOrder { get; set; }
        public Core.Enums.SearchContentType SearchContentType { get; set; }
        public Core.Enums.Category Categories { get; set; }
        public int PageSize{ get; set; }
        public int PageNumber{ get; set; }
    }
}