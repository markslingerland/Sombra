using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Shared
{
    public class Story
    {
        public Guid StoryKey { get; set; }
        public Guid CharityKey { get; set; }

        public Guid? AuthorUserKey { get; set; }
        public string AuthorName { get; set; }
        public string AuthorProfileImage { get; set; }
        public bool IsApproved { get; set; }
        public string UrlComponent { get; set; }

        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string OpeningText { get; set; }
        public string StoryImage { get; set; }
        public string CoreText { get; set; }
        public string QuoteText { get; set; }
        public string ConclusionText { get; set; }
        public DateTime Created { get; set; }
        public List<string> Images { get; set; }
    }
}