using System.Collections.Generic;

namespace Sombra.Messaging.Responses.Story
{
    public class GetRandomStoriesResponse : Response
    {
        public bool IsSuccess { get; set; }
        public List<Shared.Story> Results { get; set; }
    }
}