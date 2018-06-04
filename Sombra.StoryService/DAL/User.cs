using System;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class User : Entity
    {
        public Guid UserKey { get; set; }
        public string Name { get; set; }
    }
}