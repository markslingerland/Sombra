using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class DonationsContext : SombraContext<DonationsContext>
    {
        public DonationsContext() { }

        public DonationsContext(DbContextOptions<DonationsContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }
    }
}