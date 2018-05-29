using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Events.Email;

namespace Sombra.EmailService.UnitTests
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public async Task SendTest()
        {
            // Arrange
            var smtpClientMock = new Mock<SmtpClient>();
            smtpClientMock.Setup(m => m.DisconnectAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            smtpClientMock.Setup(m => m.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>())).Returns(Task.FromResult(true));

            var mailboxTestTo = new EmailAddress("TestRecipient", "TestRecipient@Test.com");
            var mailboxTestFrom = new EmailAddress("TestSender", "TestSender@Test.com");
            var subjectTest = "Test Subject";
            var contentTest = "Test Content";

            var emailMessage = new EmailEvent(mailboxTestFrom, mailboxTestTo, subjectTest, contentTest, true);
            var emailService = new EmailEventHandler(smtpClientMock.Object);
            // Act
            await emailService.ConsumeAsync(emailMessage);

            // Assert
            smtpClientMock.Verify(m => m.DisconnectAsync(It.Is<bool>(p => true), It.IsAny<CancellationToken>()), Times.Once);
            smtpClientMock.Verify(m => m.SendAsync(It.Is<MimeMessage>(p =>
                p.Subject == subjectTest && p.HtmlBody == contentTest && p.To[0].Name == mailboxTestTo.Name && p.From[0].Name == mailboxTestFrom.Name),
                It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()), Times.Once);
        }
    }
}