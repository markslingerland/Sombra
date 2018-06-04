using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class StoryContext : SombraContext<StoryContext>
    {
        public StoryContext() { }

        public StoryContext(DbContextOptions<StoryContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }
    }
}