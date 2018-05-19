using Sombra.Infrastructure.DAL;

namespace Sombra.CharityActionService.DAL
{
    public class DesignTimeDbContextCharityActionFactory : DesignTimeDbSombraContextFactory<CharityActionContext>
    {
        protected override string ConnectionStringName { get; } = "CHARITYACTION_DB_CONNECTIONSTRING";
    }
}