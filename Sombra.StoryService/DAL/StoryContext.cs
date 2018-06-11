using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class StoryContext : SombraContext<StoryContext>
    {
        public StoryContext() { }

        public StoryContext(DbContextOptions<StoryContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }

        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Charity> Charities { get; set; }
    }
}