using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests.Template;

namespace Sombra.TemplateService.DAL
{
    public class Template : Entity
    {
        public EmailType TemplateKey { get; set; }
        public string Body { get; set; }
    }
}