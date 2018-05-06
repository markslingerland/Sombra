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
using Sombra.CharityService.DAL;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class ChangeCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task ChangeCharityRequest_Handle_Returns_Charity()
        {

            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityCreatedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                CharityResponse response;
                var request = new CharityRequest()
                {
                    CharityId = "1",
                    NameCharity = "0",
                    NameOwner = "0"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.Charity.Add(new CharityEntity
                    {
                        CharityId = "1",
                        NameCharity = "testCharity",
                        NameOwner = "testOwner"
                        
                    });

                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new ChangeCharityRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityId, context.Charity.Single().CharityId);
                    Assert.AreEqual(request.NameOwner, context.Charity.Single().NameOwner);
                    Assert.AreEqual(request.NameCharity, context.Charity.Single().NameCharity);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CharityCreatedEvent>(e => e.CharityId == request.CharityId && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}