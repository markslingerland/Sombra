using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityActionService.DAL
{
    public class CharityActionContext : SombraContext<CharityActionContext>
    {
        public CharityActionContext() { }
        public CharityActionContext(DbContextOptions<CharityActionContext> options) : base(options)
        {
        }
        public DbSet<CharityActionEntity> CharityActions { get; set; }
    }
}
