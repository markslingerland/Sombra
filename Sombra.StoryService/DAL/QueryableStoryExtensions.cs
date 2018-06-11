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

        public static IQueryable<Story> IncludeCharity(this IQueryable<Story> stories)
            => stories.Include(s => s.Charity);

        public static IQueryable<Story> IncludeAll(this IQueryable<Story> stories)
            => stories.IncludeAuthor().IncludeCharity().IncludeImages();
    }
}