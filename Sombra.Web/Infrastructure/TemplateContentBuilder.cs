using System.Collections.Generic;

namespace Sombra.Web.Infrastructure
{
    public static class TemplateContentBuilder
    {
        public static Dictionary<string, string> CreateForgotPasswordTemplateContent(string name, string actionUrl, string operatingSystem, string browserName)
        {
            return new Dictionary<string, string>
            {
                {"name", name},
                {"action_url", actionUrl},
                {"operating_system", operatingSystem},
                {"browser_name", browserName}
            };
        }

        public static Dictionary<string, string> CreateConfirmAccountTemplateContent(string name, string actionUrl)
        {
            return new Dictionary<string, string>
            {
                {"name", name},
                {"action_url", actionUrl}
            };
        }
    }
}