using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class EmailTemplateRequest : Request<EmailTemplateResponse>{
        public EmailType EmailType { get; set; }
        public Dictionary<string,string> TemplateContent { get; set;}

    }

    public enum EmailType {
        Unkown = 0,
        ForgotPasswordTemplate = 1
        
        };
}