using System.Collections.Generic;

namespace Sombra.Web.Infrastructure {
    public static class TemplateContentBuilder{
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