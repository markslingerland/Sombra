using System;
using System.Collections.Generic;
using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    public class UpdateStoryRequest : Request<UpdateStoryResponse>
    {
        public Guid StoryKey { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string OpeningText { get; set; }
        public string StoryImage { get; set; }
        public string CoreText { get; set; }
        public string QuoteText { get; set; }
        public string ConclusionText { get; set; }
        public List<string> Images { get; set; }
        public string UrlComponent { get; set; }
    }
}