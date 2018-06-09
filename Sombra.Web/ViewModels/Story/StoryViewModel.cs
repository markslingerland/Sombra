using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Story
{
    public class StoryViewModel
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserImage { get; set; }
        public string Title { get; set; }
        public string[] Story { get; set; }
        public string Quote { get; set; }
        public string MainImage { get; set; }
        public string[] Images { get; set; }
        public StoryViewModel RandomStory{ get; set; }
        public StoryViewModel[] EqualStories { get; set; }
    }
}
