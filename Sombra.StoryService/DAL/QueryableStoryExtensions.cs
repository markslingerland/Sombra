using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sombra.StoryService.DAL
{
    public static class QueryableStoryExtensions
    {
        public static IQueryable<Story> IncludeImages(this IQueryable<Story> stories)
        {
            return stories.Include(b => b.Images).Include(b => b.CoverImage).Include(b => b.StoryImage);
        }
    }
}