using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class UpdateCharityActionRequestHandlerTests
    {
        [TestMethod]
        public async Task UpdateCharityActionRequest_Handle_Returns_Charity()
        {

            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionUpdatedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }


                UpdateCharityActionResponse response;
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var request = new UpdateCharityActionRequest
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
                    var handler = new UpdateCharityActionRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityActionkey, context.CharityActions.Single().CharityActionkey);
                    Assert.AreEqual(request.Charitykey, context.CharityActions.Single().Charitykey);
                    // TODO Fix unit test problem UserKey in context returns null
                    Assert.AreEqual(request.UserKeys, context.CharityActions.Single().UserKeys);
                    Assert.AreEqual(request.NameCharity, context.CharityActions.Single().NameCharity);
                    Assert.AreEqual(request.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(request.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(request.NameAction, context.CharityActions.Single().NameAction);
                    Assert.AreEqual(request.Discription, context.CharityActions.Single().Discription);
                    Assert.AreEqual(request.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionUpdatedEvent>(e => e.CharityActionkey == request.CharityActionkey && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}
