using System;
using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    public class GetStoryByKeyRequest : Request<GetStoryByKeyResponse>
    {
        public Guid StoryKey { get; set; }
    }
}