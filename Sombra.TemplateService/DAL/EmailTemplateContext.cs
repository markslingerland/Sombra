using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.TemplateService.DAL
{
    public class EmailTemplateContext : SombraContext
    {
        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options) : base(options)
        {
        }
        public DbSet<TemplateEntity> Template { get; set; }
    }
}