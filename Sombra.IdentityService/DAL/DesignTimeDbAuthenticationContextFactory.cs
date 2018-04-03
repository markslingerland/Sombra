using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sombra.IdentityService.DAL
{
    public class DesignTimeDbContextAuthenticationFactory : IDesignTimeDbContextFactory<AuthenticationContext>
    {
        public AuthenticationContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AuthenticationContext>();
            var connectionString = configuration["AUTHENTICATION_DB_CONNECTIONSTRING"];
            builder.UseSqlServer(connectionString);

            return new AuthenticationContext(builder.Options);
        }
    }
}