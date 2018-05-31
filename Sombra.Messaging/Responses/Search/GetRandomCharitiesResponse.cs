using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses.Search
{
    public class GetRandomCharitiesResponse : Response
    {
        public List<SearchResult> Results { get; set; }
    }
}