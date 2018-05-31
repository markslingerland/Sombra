using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class DonationsContext : SombraContext<DonationsContext>
    {
        public DonationsContext() { }

        public DonationsContext(DbContextOptions<DonationsContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }

        public DbSet<Charity> Charities { get; set; }
        public DbSet<CharityAction> CharityActions { get; set; }
        public DbSet<CharityDonation> CharityDonations { get; set; }
        public DbSet<CharityActionDonation> ChartyActionDonations { get; set; }
    }
}