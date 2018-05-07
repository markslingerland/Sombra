using System.Collections.Generic;
using System.Linq;

namespace Sombra.Web.Infrastructure {
    public static class TemplateContentBuilder
    {
        public static string Build(string template, Dictionary<string, string> content) => content.Aggregate(template,
            (current, item) => current.Replace($"[[{item.Key}]]", item.Value));

        public static Dictionary<string,string> CreateForgotPasswordTempleteContent(string name, string actionUrl, string operatingSystem, string browserName){

            return new Dictionary<string,string>() {
                {"name", name},
                {"action_url", actionUrl},
                {"operating_system", operatingSystem},
                {"browser_name", browserName}
            };
        }
    }
}