using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    public class GetRandomStoriesRequest : Request<GetRandomStoriesResponse>
    {
        public int Amount { get; set; }
    }
}