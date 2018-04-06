using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;

namespace Sombra.EmailService
{
    public class EmailService : IAsyncEventHandler<Email>, IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public MimeMessage CreateEmailMessage(Email emailMessage)
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

        public async Task Send(MimeMessage message)
        {
            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(true);
            }
        }

        public async Task Consume(Email message)
        {
            var email = CreateEmailMessage(message);
            await Send(email);
        }
    }
}
