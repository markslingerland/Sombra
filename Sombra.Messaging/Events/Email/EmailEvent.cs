using System.Collections.Generic;

namespace Sombra.Messaging.Events.Email
{
    public class EmailEvent : Event
    {
        public EmailEvent() { }
        public EmailEvent(EmailAddress sender, List<EmailAddress> recipient, string subject, string body, bool isHtml = false)
        {
            Sender = sender;
            Recipient =  recipient;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }
        public EmailEvent(EmailAddress sender, EmailAddress recipient, string subject, string body, bool isHtml = false)
        {
            Sender = sender;
            Recipient = new List<EmailAddress> { recipient };
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public EmailAddress Sender { get; set; }
        public List<EmailAddress> Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
    public class EmailAddress
    {
        public EmailAddress(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}