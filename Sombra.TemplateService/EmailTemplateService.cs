using Sombra.TemplateService.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.TemplateService
{
    public class EmailTemplateService
    {
        private readonly Dictionary<string, string> _templateContent;
        private readonly EmailType _type;

        public EmailTemplateService(EmailTemplateRequest request) // + Context voeren
        {
            _type = request.EmailType;
            _templateContent = request.TemplateContent;
        }

        public string Construct()
        {
            var template = string.Empty; // Haal je template uit je context aan de hand van je _type
            foreach (var item in _templateContent)
            {
                template = template.Replace($"[[{item.Key}]]", item.Value);
            }

            return template;
        }
    }
}

        