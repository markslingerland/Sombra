using Sombra.Infrastructure.DAL;

namespace Sombra.TemplateService.DAL
{
    public class DesignTimeDbEmailTemplateContextFactory : DesignTimeDbSombraContextFactory<EmailTemplateContext>
    {
        protected override string ConnectionStringName { get; } = "TEMPLATE_DB_CONNECTIONSTRING";
    }
}