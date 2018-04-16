using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.TemplateService.Templates
{
    public class EmailTemplateRequest
    {
        public EmailType EmailType { get; set; }
        public Dictionary<string, string> TemplateContent { get; set; }
    }
}
