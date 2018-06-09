using System;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class Image : Entity
    {
        public string Base64 { get; set; }
    }
}