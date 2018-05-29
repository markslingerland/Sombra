using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.CharityActionService.DAL;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using UserKey = Sombra.CharityActionService.DAL.UserKey;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class UpdateCharityActionRequestHandlerTests
    {
        [TestMethod]
        public async Task UpdateCharityActionRequest_Handle_Updates_Charity()
        {

            CharityActionContext.OpenInMemoryConnection();

            try
            {
                //Arrange
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionUpdatedEvent>())).Returns(Task.FromResult(true));

                UpdateCharityActionResponse response;
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var key = Guid.NewGuid();
                var user = new UserKey { Key = key };
                var userMessenging = new Messaging.Shared.UserKey { Key = key };
                var request = new UpdateCharityActionRequest
                {
                    CharityActionKey = keyAction,
                    CharityKey = keyCharity,
                    UserKeys = new List<Messaging.Shared.UserKey> { userMessenging },
                    CharityName = "",
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    ActionType = "",
                    Description = "",
                    CoverImage = ""

                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = new List<UserKey> { new UserKey() { Key = Guid.NewGuid() } },
                        CharityName = "testNAmeOwner",
                        Category = Category.Dierenbescherming,
                        IBAN = "",
                        Name = "",
                        ActionType = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    });
                    context.SaveChanges();
                }
                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new UpdateCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }
                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityActionKey, context.CharityActions.Single().CharityActionKey);
                    Assert.AreEqual(request.CharityKey, context.CharityActions.Single().CharityKey);
                    CollectionAssert.AreEquivalent(request.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(request.CharityName, context.CharityActions.Single().CharityName);
                    Assert.AreEqual(request.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(request.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(request.Name, context.CharityActions.Single().Name);
                    Assert.AreEqual(request.ActionType, context.CharityActions.Single().ActionType);
                    Assert.AreEqual(request.Description, context.CharityActions.Single().Description);
                    Assert.AreEqual(request.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionUpdatedEvent>(e => e.CharityActionKey == request.CharityActionKey && e.CharityName == request.CharityName)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
        [TestMethod]
        public async Task UpdateCharityActionRequest_Handle_Returns_Null()
        {

            CharityActionContext.OpenInMemoryConnection();

            try
            {
                //Arrange
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionUpdatedEvent>())).Returns(Task.FromResult(true));

                UpdateCharityActionResponse response;
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var wrongKey = Guid.NewGuid();
                var key = Guid.NewGuid();

                var userMessenging = new Messaging.Shared.UserKey { Key = key };
                var request = new UpdateCharityActionRequest
                {
                    CharityActionKey = wrongKey,
                    CharityKey = keyCharity,
                    UserKeys = new List<Messaging.Shared.UserKey>() { userMessenging },
                    CharityName = "",
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    ActionType = "",
                    Description = "",
                    CoverImage = ""

                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = new List<UserKey>() { new UserKey() { Key = Guid.NewGuid() } },
                        CharityName = "testNAmeOwner",
                        Category = Category.Dierenbescherming,
                        IBAN = "",
                        Name = "",
                        ActionType = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    });
                    context.SaveChanges();
                }
                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new UpdateCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                    Assert.AreEqual("testNAmeOwner", context.CharityActions.Single().CharityName);
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}
