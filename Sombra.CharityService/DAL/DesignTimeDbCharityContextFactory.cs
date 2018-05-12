using Sombra.Infrastructure.DAL;

namespace Sombra.CharityService.DAL
{
    public class DesignTimeDbContextCharityFactory : DesignTimeDbSombraContextFactory<CharityContext>
    {
        protected override string ConnectionStringName { get; } = "CHARITY_DB_CONNECTIONSTRING";
    }
}