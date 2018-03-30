namespace Sombra.Messaging.Events
{
    public class Email : Event
    {
        public Email(string sender, string recipient, string subject, string body, bool isHtml = false)
        {
            Sender = sender;
            Recipient = recipient;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}