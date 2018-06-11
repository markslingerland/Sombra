using System.Collections.Generic;

namespace Sombra.Messaging.Responses.Story
{
    public class GetStoriesResponse : Response
    {
        public List<Shared.Story> Results { get; set; }
        public int TotalNumberOfResults { get; set; }
    }
}