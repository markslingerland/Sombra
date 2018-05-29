using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests.Template;

namespace Sombra.TemplateService.DAL
{
    public class TemplateEntity : Entity
    {
        public EmailType TemplateId { get; set; }
        public string Template { get; set; }
    }
}