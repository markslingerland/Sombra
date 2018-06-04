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

            var template = await _emailTemplateContext.Templates.FirstOrDefaultAsync(b => b.TemplateKey.Equals(message.EmailType));
            if (template?.Body == null) return new EmailTemplateResponse();
            response.Template = template.Body;

            return response;
        }
    }
}