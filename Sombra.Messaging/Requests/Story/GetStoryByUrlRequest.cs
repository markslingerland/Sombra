using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    public class GetStoryByUrlRequest : Request<GetStoryByUrlResponse>
    {
        public string CharityUrl { get; set; }
        public string StoryUrlComponent { get; set; }
    }
}