using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests;

namespace Sombra.TemplateService.DAL
{
    public class TemplateEntity : Entity
    {
        public EmailType TemplateId { get; set; }
        public string Template { get; set; }
    }
}