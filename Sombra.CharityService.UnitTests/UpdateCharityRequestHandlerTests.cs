using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using Sombra.Infrastructure;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class UpdateCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task UpdateCharityRequest_Handle_Returns_Charity()
        {

            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityUpdatedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                UpdateCharityResponse response;
                var newKey = Guid.NewGuid();
                var request = new UpdateCharityRequest()
                {
                    CharityKey = newKey,
                    Name = "0",
                    OwnerUserName = "0",
                    Email = "0",
                    Category = Core.Enums.Category.None,
                    KVKNumber = "",
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "0"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.Charities.Add(new Charity
                    {
                        CharityKey = newKey,
                        Name = "0",
                        OwnerUserName = "0",
                        Email = "testEmail",
                        Category = Core.Enums.Category.Dierenbescherming,
                        KVKNumber = "1",
                        IBAN = "1111-1111",
                        CoverImage = "x",
                        Slogan = "Test2"

                    });

                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new UpdateCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
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
                busMock.Verify(m => m.PublishAsync(It.Is<CharityUpdatedEvent>(e => e.CharityKey == request.CharityKey && e.Name == request.Name)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}