using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sombra.UserService.DAL
{
    public class DesignTimeDbContextUserFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<UserContext>();
            var connectionString = configuration["USER_DB_CONNECTIONSTRING"];
            builder.UseSqlServer(connectionString);

            return new UserContext(builder.Options);
        }
    }
}