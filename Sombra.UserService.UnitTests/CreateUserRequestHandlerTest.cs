using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService.UnitTests
{
    [TestClass]
    public class CreateUserRequestHandlerTest
    {
        [TestMethod]
        public async Task CreateUserRequestHandler_Handle_Returns_Success()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserCreatedEvent>())).Returns(Task.FromResult(true));

                CreateUserResponse response;
                var request = new CreateUserRequest
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new CreateUserRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreEqual(request.UserKey, context.Users.Single().UserKey);
                    Assert.AreEqual(request.FirstName, context.Users.Single().FirstName);
                    Assert.AreEqual(request.LastName, context.Users.Single().LastName);
                    Assert.AreEqual(request.AddressLine1, context.Users.Single().AddressLine1);
                    Assert.IsNotNull(context.Users.Single().Created);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<UserCreatedEvent>(e => e.UserKey == request.UserKey && e.FirstName == request.FirstName)), Times.Once);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateUserRequestHandler_Handle_Returns_KeyEmpty()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserCreatedEvent>())).Returns(Task.FromResult(true));

                CreateUserResponse response;
                var request = new CreateUserRequest
                {
                    FirstName = "John",
                    LastName = "Doe"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new CreateUserRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.AreEqual(0, context.Users.Count());
                    Assert.AreEqual(ErrorType.InvalidKey, response.ErrorType);
                    Assert.IsFalse(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.IsAny<UserCreatedEvent>()), Times.Never());
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateUserRequestHandler_Handle_Returns_EmailExists()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserCreatedEvent>())).Returns(Task.FromResult(true));

                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    EmailAddress = "john@doe.com"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                CreateUserResponse response;
                var request = new CreateUserRequest
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john@doe.com"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new CreateUserRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreEqual(ErrorType.EmailExists, response.ErrorType);
                    Assert.IsFalse(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.IsAny<UserCreatedEvent>()), Times.Never());
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }
    }
}