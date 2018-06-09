using System;
using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    public class ApproveStoryRequest : Request<ApproveStoryResponse>
    {
        public Guid StoryKey { get; set; }
    }
}