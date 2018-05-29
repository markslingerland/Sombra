using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class GetSearchResultResponse : Response
    {
        public List<SearchResult> Results { get; set; }
        public int TotalResult { get; set; }
    }

    public class SearchResult
    {
        public System.Guid CharityKey { get; set; }
        public System.Guid CharityActionKey { get; set; }
        public string CharityName { get; set; }
        public string CharityActionName { get; set; }
        public Core.Enums.SearchContentType Type { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Core.Enums.Category Category { get; set; }
    }
}