using System.Collections.Generic;

namespace Sombra.Web.ViewModels.Story
{
    public class StoryViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string OpeningText { get; set; }
        public string StoryImage { get; set; }
        public string CoreText { get; set; }
        public string QuoteText { get; set; }
        public string ConclusionText { get; set; }
        public List<string> Images { get; set; }
    }
}