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
                var key = Guid.NewGuid();
                var userMessenging = new Messaging.Shared.UserKey { Key = key };
                var request = new UpdateCharityActionRequest
                {
                    CharityActionKey = keyAction,
                    UserKeys = new List<Messaging.Shared.UserKey> { userMessenging },
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    Description = "",
                    CoverImage = ""

                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid()
                    };

                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        UserKeys = new List<UserKey> { new UserKey { Key = Guid.NewGuid() } },
                        Category = Category.AnimalProtection,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = "",
                        Charity = charity
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
                    CollectionAssert.AreEquivalent(request.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(request.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(request.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(request.Name, context.CharityActions.Single().Name);
                    Assert.AreEqual(request.Description, context.CharityActions.Single().Description);
                    Assert.AreEqual(request.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.IsTrue(response.IsSuccess);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionUpdatedEvent>(e => e.CharityActionKey == request.CharityActionKey)), Times.Once);
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
                var wrongKey = Guid.NewGuid();
                var key = Guid.NewGuid();

                var userMessenging = new Messaging.Shared.UserKey { Key = key };
                var request = new UpdateCharityActionRequest
                {
                    CharityActionKey = wrongKey,
                    UserKeys = new List<Messaging.Shared.UserKey>() { userMessenging },
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    Description = "",
                    CoverImage = ""

                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid()
                    };

                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        UserKeys = new List<UserKey> { new UserKey { Key = Guid.NewGuid() } },
                        Category = Category.AnimalProtection,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = "",
                        Charity = charity
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
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}
