using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using MimeKit;
using Sombra.Messaging.Events;

namespace Sombra.EmailService.UnitTest
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void SendTest()
        {
            // Arrange
            var emailConfigurationMok = new EmailConfiguration(){ SmtpServer = "", SmtpPort = 465, SmtpUsername = "ikdoneernu@gmail.com" };
            var mailboxTestTo = new EmailAddress("TestRecipient", "TestRecipient@Test.com");
            var mailboxTestFrom = new EmailAddress("TestSender", "TestSender@Test.com");
            var subjectTest = "Test Subject";
            var contentTest = "Test Content";

            var emailMessage = new Email(mailboxTestFrom, mailboxTestTo, subjectTest, contentTest, true);
            var emailService = new EmailService(emailConfigurationMok);
            // Act
            var createdMessageResult = emailService.CreateEmailMessage(emailMessage);

            // Assert
            Assert.IsTrue(createdMessageResult.Subject.Contains(subjectTest));
            Assert.IsTrue(createdMessageResult.HtmlBody.Contains(contentTest));
            Assert.IsTrue(createdMessageResult.To[0].Name.Contains(mailboxTestTo.Name));
            Assert.IsTrue(createdMessageResult.From[0].Name.Contains(mailboxTestFrom.Name));           
        }

    }
}
