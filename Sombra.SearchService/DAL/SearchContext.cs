using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class SearchContext : SombraContext<SearchContext>
    {
        public SearchContext() { }

        public SearchContext(DbContextOptions<SearchContext> options, bool seed) : base(options, seed) { }

        public DbSet<Content> Content { get; set; }
    }
}