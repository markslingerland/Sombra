﻿using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;
using System;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class CreateCharityActionRequestHandlerTests
    {
        [TestMethod]
        public async Task CreateCharityActionRequest_Handle_Returns_Success()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<CharityActionCreatedEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var userRequest = new Sombra.Messaging.UserKey() { Key = Guid.NewGuid() };
                var request = new CreateCharityActionRequest
                {
                    CharityActionkey = keyAction,
                    Charitykey = keyCharity,
                    UserKeys = new Collection<Sombra.Messaging.UserKey>() { userRequest },
                    NameCharity = "testNAmeOwner",
                    Category = Core.Enums.Category.None,
                    IBAN = "",
                    NameAction = "",
                    Discription = "0-IBAN",
                    CoverImage = ""

                };
                CreateCharityActionResponse response;
                


                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityActionRequestHandler(context, Helper.GetMapper(), busMock.Object);
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

                busMock.Verify(m => m.PublishAsync(It.Is<CharityActionCreatedEvent>(e => e.CharityActionkey == request.CharityActionkey && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

    }
}
