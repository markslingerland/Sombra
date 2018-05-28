using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityService.DAL
{
    public class CharityContext : SombraContext<CharityContext>
    {
        public CharityContext() { }
        public CharityContext(DbContextOptions<CharityContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }
        public DbSet<Charity> Charities { get; set; }
    }
}
