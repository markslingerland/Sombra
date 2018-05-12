using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL
{
    public class DesignTimeDbContextUserFactory : DesignTimeDbSombraContextFactory<UserContext>
    {
        protected override string ConnectionStringName { get; } = "USER_DB_CONNECTIONSTRING";
    }
}