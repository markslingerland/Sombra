using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class Story : Entity
    {
        public Guid StoryKey { get; set; }
        public Guid CharityKey { get; set; }
        public string CharityName { get; set; }
        public virtual User Author { get; set; }
        public Guid? AuthorId { get; set; }
        public bool IsApproved { get; set; }

        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string OpeningText { get; set; }
        public string StoryImage { get; set; }
        public string CoreText { get; set; }
        public string QuoteText { get; set; }
        public string ConclusionText { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public DateTime Created { get; set; }
    }
}