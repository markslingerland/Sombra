using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sombra.TemplateService.DAL
{
    public class DesignTimeDbContextUserFactory : IDesignTimeDbContextFactory<EmailTemplateContext>
    {
        public EmailTemplateContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<EmailTemplateContext>();
            var connectionString = configuration["TEMPLATE_DB_CONNECTIONSTRING"];
            builder.UseSqlServer(connectionString);

            return new EmailTemplateContext(builder.Options);
        }
    }
}