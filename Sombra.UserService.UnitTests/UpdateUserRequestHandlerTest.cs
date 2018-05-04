using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Core.Enums;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService.UnitTests
{
    [TestClass]
    public class UpdateUserRequestHandlerTest
    {
        [TestMethod]
        public async Task UpdateUserRequestHandler_Handle_Returns_User()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserUpdatedEvent>())).Returns(Task.FromResult(true));

                UpdateUserResponse response;
                var request = new UpdateUserRequest()
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        UserKey = request.UserKey,
                        FirstName = "John",
                        LastName = request.LastName
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new UpdateUserRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreEqual(request.UserKey, context.Users.Single().UserKey);
                    Assert.AreEqual(request.FirstName, context.Users.Single().FirstName);
                    Assert.AreEqual(request.LastName, context.Users.Single().LastName);
                    Assert.AreEqual(request.AddressLine1, context.Users.Single().AddressLine1);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<UserUpdatedEvent>(e => e.UserKey == request.UserKey && e.FirstName == request.FirstName)), Times.Once);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UpdateUserRequestHandler_Handle_Returns_UserKeyEmpty()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserUpdatedEvent>())).Returns(Task.FromResult(true));

                UpdateUserResponse response;
                var request = new UpdateUserRequest()
                {
                    FirstName = "Ellen",
                    LastName = "Doe"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        UserKey = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = request.LastName
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new UpdateUserRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreNotEqual(request.FirstName, context.Users.Single().FirstName);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                }

                busMock.Verify(m => m.PublishAsync(It.IsAny<UserUpdatedEvent>()), Times.Never());
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public async Task UpdateUserRequestHandler_Handle_Returns_UserEmailExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UserUpdatedEvent>())).Returns(Task.FromResult(true));

                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                UpdateUserResponse response;
                var request = new UpdateUserRequest()
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe",
                    EmailAddress = "ellen@doe.com"
                };

                using (var context = new UserContext(options, false))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        UserKey = request.UserKey,
                        FirstName = "John",
                        LastName = request.LastName,
                        EmailAddress = "john@doe.com"
                    });

                    context.Users.Add(new User
                    {
                        UserKey = Guid.NewGuid(),
                        EmailAddress = "ellen@doe.com"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options, false))
                {
                    var handler = new UpdateUserRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = new UserContext(options, false))
                {
                    Assert.AreEqual(2, context.Users.Count());
                    Assert.AreNotEqual(request.EmailAddress, context.Users.Single(u => u.UserKey == request.UserKey).EmailAddress);
                    Assert.AreEqual(ErrorType.EmailExists, response.ErrorType);
                    Assert.IsFalse(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.IsAny<UserUpdatedEvent>()), Times.Never());
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }
    }
}