using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sombra.CharityService.DAL
{
    public class DesignTimeDbContextCharityFactory : IDesignTimeDbContextFactory<CharityContext>
    {
        public CharityContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CharityContext>();
            var connectionString = configuration["CHARITY_DB_CONNECTIONSTRING"];
            builder.UseSqlServer(connectionString);

            return new CharityContext(builder.Options);
        }
    }
}