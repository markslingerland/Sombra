using Sombra.Messaging.Responses.Template;

namespace Sombra.Messaging.Requests.Template
{
    [Cachable]
    public class EmailTemplateRequest : Request<EmailTemplateResponse>
    {
        public EmailTemplateRequest()
        {
        }

        public EmailTemplateRequest(EmailType emailType)
        {
            EmailType = emailType;
        }

        [CacheKey]
        public EmailType EmailType { get; set; }
    }

    public enum EmailType
    {
        Unkown = 0,
        ForgotPassword = 1,
        ConfirmAccount = 2
    }
}