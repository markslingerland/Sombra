using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;

namespace Sombra.EmailService
{
    public class EmailEventHandler : IAsyncEventHandler<EmailEvent>
    {
        private readonly SmtpClient _smtpClient;

        public EmailEventHandler(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        private MimeMessage CreateEmailMessage(EmailEvent emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.Recipient.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.Add(new MailboxAddress(emailMessage.Sender.Name, emailMessage.Sender.Address));
            message.Subject = emailMessage.Subject;
            var bodyBuilder = new BodyBuilder();
            if (emailMessage.IsHtml)
            {
                bodyBuilder.HtmlBody = emailMessage.Body;
            }
            else
            {
                bodyBuilder.TextBody = emailMessage.Body;
            }
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }

        public async Task ConsumeAsync(EmailEvent message)
        {
            var email = CreateEmailMessage(message);
            await _smtpClient.SendAsync(email);
            await _smtpClient.DisconnectAsync(true);
        }
    }
}