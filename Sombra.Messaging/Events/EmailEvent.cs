using System.Collections.Generic;

namespace Sombra.Messaging.Events
{
    public class EmailEvent : Event
    {
<<<<<<< HEAD:Sombra.Messaging/Events/Email.cs
        public Email() { }

        public Email(EmailAddress sender, EmailAddress recipient, string subject, string body, bool isHtml = false)
=======
        public EmailEvent() { }
        public EmailEvent(EmailAddress sender, List<EmailAddress> recipient, string subject, string body, bool isHtml = false)
>>>>>>> master:Sombra.Messaging/Events/EmailEvent.cs
        {
            Sender = sender;
            Recipient = new List<EmailAddress> { recipient };
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }
<<<<<<< HEAD:Sombra.Messaging/Events/Email.cs
        
        public Email(EmailAddress sender, List<EmailAddress> recipient, string subject, string body, bool isHtml = false)
=======
        public EmailEvent(EmailAddress sender, EmailAddress recipient, string subject, string body, bool isHtml = false)
>>>>>>> master:Sombra.Messaging/Events/EmailEvent.cs
        {
            Sender = sender;
            Recipient = recipient;
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