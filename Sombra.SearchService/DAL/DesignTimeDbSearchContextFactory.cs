using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sombra.SearchService.DAL
{
    public class DesignTimeDbSearchContextFactory
    {
        
        public SearchContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SearchContext>();
            var connectionString = configuration["SEARCH_DB_CONNECTIONSTRING"];
            builder.UseSqlServer(connectionString);

            return new SearchContext(builder.Options);
        }
    }
    
}