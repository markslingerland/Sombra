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
using Sombra.Infrastructure;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class DeleteCharityActionByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task DeleteCharityActionByKeyRequest_Handle_Deletes_CharityAction()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                //Arrange
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
                    CharityActionKey = keyAction,
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = new Collection<UserKey>() { new UserKey() { Key = Guid.NewGuid() } },
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.Dierenbescherming,
                        IBAN = "",
                        NameAction = "",
                        ActionType = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    });

                    context.SaveChanges();
                }
                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new DeleteCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.CharityActions.Any(i => i.CharityActionKey == request.CharityActionKey));
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionDeletedEvent>(e => e.CharityActionKey == request.CharityActionKey)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task DeleteCharityActionByKeyRequest_Handle_Returns_Null()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                //Arrange
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionDeletedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                DeleteCharityActionResponse response;
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var wrongKey = Guid.NewGuid();
                var request = new DeleteCharityActionRequest
                {
                    CharityActionKey = wrongKey,
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.CharityActions.Add(new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = new Collection<UserKey>() { new UserKey() { Key = Guid.NewGuid() } },
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.Dierenbescherming,
                        IBAN = "",
                        NameAction = "",
                        ActionType = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    });

                    context.SaveChanges();
                }
                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new DeleteCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.IsFalse(response.Success);               
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}