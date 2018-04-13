using MimeKit;
using Sombra.Messaging.Events;
using System.Threading.Tasks;

namespace Sombra.EmailService
{
    public interface IEmailService
    {
        MimeMessage CreateEmailMessage(Email emailMessage);
        Task Send(MimeMessage message);
    }

}
