using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.EmailService
{
    public interface IEmailService
    {
        MimeMessage CreateEmailMessage(EmailMessage emailMessage);
        void Send(MimeMessage message);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }

}
