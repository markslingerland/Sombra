using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.TemplateService.DAL;
using System.Linq;
using System.Threading.Tasks;

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
            var templateBody = template.Template;

            foreach (var item in message.TemplateContent)
            {
                templateBody = templateBody.Replace($"[[{item.Key}]]", item.Value);
            }

            response.Template = templateBody;

            return response;
        }
    }
}