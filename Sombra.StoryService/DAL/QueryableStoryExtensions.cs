using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sombra.StoryService.DAL
{
    public static class QueryableStoryExtensions
    {
        public static IQueryable<Story> IncludeImages(this IQueryable<Story> stories)
            => stories.Include(s => s.Images);

        public static IQueryable<Story> IncludeAuthor(this IQueryable<Story> stories)
            => stories.Include(s => s.Author);
    }
}