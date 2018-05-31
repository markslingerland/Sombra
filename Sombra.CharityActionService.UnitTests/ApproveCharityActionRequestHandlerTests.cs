using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.CharityActionService.DAL;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class ApproveCharityActionRequestHandlerTests
    {
        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_Success()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityActionResponse response;
                var request = new ApproveCharityActionRequest
                {
                    CharityActionKey = Guid.NewGuid()
                };
                var charity = new CharityAction
                {
                    CharityActionKey = request.CharityActionKey,
                    Name = "0",
                    Category = Category.AnimalProtection,
                    IBAN = "1111-1111",
                    CoverImage = "x"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.CharityActions.Single().IsApproved);
                    Assert.IsTrue(response.Success);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionCreatedEvent>(e => e.CharityActionKey == request.CharityActionKey)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_AlreadyActive()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityActionResponse response;
                var request = new ApproveCharityActionRequest
                {
                    CharityActionKey = Guid.NewGuid()
                };
                var charity = new CharityAction
                {
                    CharityActionKey = request.CharityActionKey,
                    IsApproved = true,
                    Name = "0",
                    Category = Category.AnimalProtection,
                    IBAN = "1111-1111",
                    CoverImage = "x"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.CharityActions.Single().IsApproved);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.AlreadyActive, response.ErrorType);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionCreatedEvent>(e => e.CharityActionKey == request.CharityActionKey)), Times.Never);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_NotFound()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityActionResponse response;
                var request = new ApproveCharityActionRequest
                {
                    CharityActionKey = Guid.NewGuid()
                };
                var charity = new CharityAction
                {
                    CharityActionKey = Guid.NewGuid(),
                    CharityKey = Guid.NewGuid(),
                    Name = "0",
                    Category = Category.AnimalProtection,
                    IBAN = "1111-1111",
                    CoverImage = "x",
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.CharityActions.Single().IsApproved);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionCreatedEvent>(e => e.CharityActionKey == request.CharityActionKey)), Times.Never);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}