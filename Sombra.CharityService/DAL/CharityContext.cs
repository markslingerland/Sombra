using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityService.DAL
{
    public class CharityContext : SombraContext<CharityContext>
    {
        public CharityContext() { }
        public CharityContext(DbContextOptions<CharityContext> options) : base(options)
        {
        }
        public DbSet<CharityEntity> Charities { get; set; }
    }
}
