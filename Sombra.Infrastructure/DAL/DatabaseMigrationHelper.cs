using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;

namespace Sombra.Infrastructure.DAL
{
    public static class DatabaseMigrationHelper
    {
        public static void ForceMigrations(ServiceProvider serviceProvider)
        {
            var connectionStrings = serviceProvider.GetServices<SqlConnectionStringWrapper>();

            foreach (var connectionString in connectionStrings)
            {
                ExtendedConsole.Log($"Forcing migrations for {connectionString.ContextType.Name}");
                using (var context = (SombraContext) serviceProvider.GetRequiredService(connectionString.ContextType))
                {
                    ExtendedConsole.Log($"Finished forcing migrations for {connectionString.ContextType.Name}");
                }
            }
        }
    }
}