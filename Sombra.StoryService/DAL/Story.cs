using System;
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
    }
}