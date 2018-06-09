using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class DesignTimeDbDonationsContextFactory : DesignTimeDbSombraContextFactory<DonationsContext>
    {
        protected override string ConnectionStringName { get; } = "DONATIONS_DB_CONNECTIONSTRING";
    }
}