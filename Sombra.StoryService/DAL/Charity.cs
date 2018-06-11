using System;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class Charity : Entity
    {
        public Guid CharityKey { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}