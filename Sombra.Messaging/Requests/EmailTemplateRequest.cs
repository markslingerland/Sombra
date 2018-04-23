using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class EmailTemplateRequest : Request<EmailTemplateResponse>{
        
        public EmailTemplateRequest(){
            
        }
        public EmailTemplateRequest(EmailType emailType, Dictionary<string,string> templateContent){
            EmailType = emailType;
            TemplateContent = templateContent;
        }
        public EmailType EmailType { get; set; }
        public Dictionary<string,string> TemplateContent { get; set;}

    }

    public enum EmailType {
        Unkown = 0,
        ForgotPasswordTemplate = 1
        
        };
}