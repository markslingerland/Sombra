using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.TemplateService.DAL;
using System.Threading.Tasks;
using Sombra.Messaging.Requests.Template;
using Sombra.Messaging.Responses.Template;

namespace Sombra.TemplateService
{
    public class EmailTemplateRequestHandler : IAsyncRequestHandler<EmailTemplateRequest, EmailTemplateResponse>
    {
        private readonly EmailTemplateContext _emailTemplateContext;

        public EmailTemplateRequestHandler(EmailTemplateContext emailTemplateContext)
        {
            _emailTemplateContext = emailTemplateContext;
        }

        public async Task<EmailTemplateResponse> Handle(EmailTemplateRequest message)
        {
            var response = new EmailTemplateResponse();

            var template = await _emailTemplateContext.Template.FirstOrDefaultAsync(b => b.TemplateId.Equals(message.EmailType));
            if (template?.Template == null) return new EmailTemplateResponse();
            response.Template = template.Template;

            return response;
        }
    }
}