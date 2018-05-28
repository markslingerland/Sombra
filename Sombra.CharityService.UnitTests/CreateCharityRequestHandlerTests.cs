using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using System;
using Sombra.Infrastructure;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class CreateCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task CreateCharityRequest_Handle_Returns_Success()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityCreatedEvent>())).Returns(Task.FromResult(true));

                CreateCharityResponse response;
                var request = new CreateCharityRequest
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "testName",
                    OwnerUserName = "testOwnerUserName",
                    Email = "test@test.com",
                    Category = Core.Enums.Category.None,
                    KVKNumber = "",
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "Test"

                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(request.Name, context.Charities.Single().Name);
                    Assert.AreEqual(request.OwnerUserName, context.Charities.Single().OwnerUserName);
                    Assert.AreEqual(request.Email, context.Charities.Single().Email);
                    Assert.AreEqual(request.Category, context.Charities.Single().Category);
                    Assert.AreEqual(request.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(request.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(request.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(request.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }
                busMock.Verify(m => m.PublishAsync(It.Is<CharityCreatedEvent>(e => e.CharityKey == request.CharityKey && e.Name == request.Name)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

    }
}
