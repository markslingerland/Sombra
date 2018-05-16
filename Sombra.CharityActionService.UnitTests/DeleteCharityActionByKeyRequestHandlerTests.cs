using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;
using Sombra.Messaging.Events;
using EasyNetQ;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class DeleteCharityActionByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task DeleteCharityActionByKeyRequest_Handle_Returns_CharityAction()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionDeletedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                DeleteCharityActionResponse response;
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var request = new DeleteCharityActionRequest
                {
                    CharityActionkey = keyAction,
                    Charitykey = keyCharity,
                    UserKeys = new Collection<Sombra.Messaging.UserKey>() { new Sombra.Messaging.UserKey() { Key = Guid.NewGuid() } },
                    NameCharity = "",
                    Category = Core.Enums.Category.None,
                    IBAN = "",
                    NameAction = "",
                    Discription = "",
                    CoverImage = ""

                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.CharityActions.Add(new CharityActionEntity
                    {
                        CharityActionkey = keyAction,
                        Charitykey = keyCharity,
                        UserKeys = new Collection<UserKey>() { new UserKey() { Key = Guid.NewGuid() } },
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.Dierenbescherming,
                        IBAN = "",
                        NameAction = "",
                        Discription = "0-IBAN",
                        CoverImage = ""

                    });

                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new DeleteCharityActionRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityActionkey, context.CharityActions.Single().CharityActionkey);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionDeletedEvent>(e => e.CharityActionkey == request.CharityActionkey && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}