﻿using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using System;

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
                busMock.Setup(m => m.PublishAsync(It.IsAny<CreatedCharityEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                CreateCharityResponse response;
                var request = new CreateCharityRequest
                {
                    CharityKey = Guid.NewGuid(),
                    NameCharity = "testNameCharity",
                    NameOwner = "testNAmeOwner",
                    EmailCharity = "test@test.com",
                    Category = Core.Enums.Category.None,
                    KVKNumber = 0,
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "Test"

                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(request.NameCharity, context.Charities.Single().NameCharity);
                    Assert.AreEqual(request.NameOwner, context.Charities.Single().NameOwner);
                    Assert.AreEqual(request.EmailCharity, context.Charities.Single().EmailCharity);
                    Assert.AreEqual(request.Category, context.Charities.Single().Category);
                    Assert.AreEqual(request.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(request.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(request.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(request.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<CreatedCharityEvent>(e => e.CharityKey == request.CharityKey && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

    }
}