using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class GetRandomCharitiesResponse : Response
    {
        public List<SearchResult> Results { get; set; }
    }
}