using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.TemplateService.Templates.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var template = await _emailTemplateContext.Template.Where(b => b.TemplateId.Equals(message.EmailType)).Select(a => a.Template).FirstOrDefaultAsync();
            if (template == null) return new EmailTemplateResponse();

            foreach (var item in message.TemplateContent)
            {
                template = template.Replace($"[[{item.Key}]]", item.Value);
            }

            response.Template = template;

            return response;
        }
    }
}
