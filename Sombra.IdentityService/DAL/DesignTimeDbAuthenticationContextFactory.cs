using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class DesignTimeDbContextAuthenticationFactory : DesignTimeDbSombraContextFactory<AuthenticationContext>
    {
        protected override string ConnectionStringName { get; } = "AUTHENTICATION_DB_CONNECTIONSTRING";
    }
}