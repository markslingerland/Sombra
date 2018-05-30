using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.CharityService.DAL;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class ApproveCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_Success()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityResponse response;
                var request = new ApproveCharityRequest
                {
                    CharityKey = Guid.NewGuid()
                };
                var charity = new Charity
                {
                    CharityKey = request.CharityKey,
                    Name = "0",
                    OwnerUserName = "0",
                    Email = "testEmail",
                    Category = Category.Dierenbescherming,
                    KVKNumber = "1",
                    IBAN = "1111-1111",
                    CoverImage = "x",
                    Slogan = "Test2"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Charities.Single().IsApproved);
                    Assert.IsTrue(response.Success);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityCreatedEvent>(e => e.CharityKey == request.CharityKey)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_AlreadyActive()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityResponse response;
                var request = new ApproveCharityRequest
                {
                    CharityKey = Guid.NewGuid()
                };
                var charity = new Charity
                {
                    CharityKey = request.CharityKey,
                    IsApproved = true,
                    Name = "0",
                    OwnerUserName = "0",
                    Email = "testEmail",
                    Category = Category.Dierenbescherming,
                    KVKNumber = "1",
                    IBAN = "1111-1111",
                    CoverImage = "x",
                    Slogan = "Test2"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Charities.Single().IsApproved);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.AlreadyActive, response.ErrorType);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityCreatedEvent>(e => e.CharityKey == request.CharityKey)), Times.Never);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveCharityRequest_Handle_Returns_NotFound()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityCreatedEvent>())).Returns(Task.FromResult(true));

                ApproveCharityResponse response;
                var request = new ApproveCharityRequest
                {
                    CharityKey = Guid.NewGuid()
                };
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "0",
                    OwnerUserName = "0",
                    Email = "testEmail",
                    Category = Category.Dierenbescherming,
                    KVKNumber = "1",
                    IBAN = "1111-1111",
                    CoverImage = "x",
                    Slogan = "Test2"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new ApproveCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Charities.Single().IsApproved);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityCreatedEvent>(e => e.CharityKey == request.CharityKey)), Times.Never);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}