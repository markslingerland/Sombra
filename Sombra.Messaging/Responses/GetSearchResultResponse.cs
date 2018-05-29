using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses
{
    public class GetSearchResultResponse : Response
    {
        public List<SearchResult> Results { get; set; }
        public int TotalResult { get; set; }
    }
}