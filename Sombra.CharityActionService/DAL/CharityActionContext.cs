using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityActionService.DAL
{
    public class CharityActionContext : SombraContext<CharityActionContext>
    {
        public CharityActionContext() { }
        public CharityActionContext(DbContextOptions<CharityActionContext> options, bool seed = false) : base(options, seed)
        {
        }
        public DbSet<CharityAction> CharityActions { get; set; }
        public DbSet<UserKey> UserKeys { get; set; }
    }
}
