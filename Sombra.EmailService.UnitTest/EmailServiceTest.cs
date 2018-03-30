using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using MimeKit;

namespace Sombra.EmailService.UnitTest
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void SendTest()
        {
            // Arrange
            IEmailConfiguration emailConfigurationMok = new EmailConfiguration(){ SmtpServer = "", SmtpPort = 465, SmtpUsername = "ikdoneernu@gmail.com",
                SmtpPassword = "", PopServer = "popserver", PopPort = 995, PopUsername = "popusername", PopPassword = "poppassword" };

            EmailService emailService = new EmailService(emailConfigurationMok);
            List<EmailAddress> toAddress = new List<EmailAddress>(){new EmailAddress(){
                    Name = "TesterTo", Address = "testTo@test.nl" } };
            List<EmailAddress> fromAddress = new List<EmailAddress>(){new EmailAddress(){
                    Name = "TesterFrom", Address = "testFrom@test.nl" } };

            // Test Variables
            MailboxAddress mailboxTestTo = new MailboxAddress(toAddress[0].Name, toAddress[0].Address);
            MailboxAddress mailboxTestFrom = new MailboxAddress(fromAddress[0].Name, fromAddress[0].Address);
            string subjectTest = "Test Subject";
            string contentTest = "Test Content";

            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = toAddress,
                FromAddresses = fromAddress,
                Subject = subjectTest,
                Content = contentTest
            };

            // Act
            MimeMessage createdMessageResult = emailService.CreateEmailMessage(emailMessage);

            // Assert
            Assert.IsTrue(createdMessageResult.Subject.Contains(subjectTest));
            Assert.IsTrue(createdMessageResult.HtmlBody.Contains(contentTest));
            Assert.IsTrue(createdMessageResult.To[0].Name.Contains(mailboxTestTo.Name));
            Assert.IsTrue(createdMessageResult.From[0].Name.Contains(mailboxTestFrom.Name));           
            // Assert.AreEqual(createdMessage.To[0].Name, mailboxTestTo.Name); adresses are encoded not sure how to test this value to have changed to our input
            // Assert.AreEqual(createdMessage.From[0].Encoding, mailboxTestFrom.Address); adresses are encoded not sure how to test this value to have changed to our input

        }

    }
}
