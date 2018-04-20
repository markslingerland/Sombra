using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.TemplateService.Templates.DAL
{
    public class TemplateEntity : Entity
    {
        public EmailType TemplateId { get; set; }
        public string Template { get; set; }
    }
}
