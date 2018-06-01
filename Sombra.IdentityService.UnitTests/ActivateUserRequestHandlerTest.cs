using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class ActivateUserRequestHandlerTest
    {
        [TestMethod]
        public async Task ActivateUserRequestHandler_Handle_Returns_TokenInvalid()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        ActivationToken = "token",
                        ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1)
                    });
                    context.SaveChanges();
                }

                ActivateUserResponse response;
                var request = new ActivateUserRequest
                {
                    ActivationToken = "testtoken"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new ActivateUserRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Users.Single().IsActive);
                    Assert.AreEqual(ErrorType.TokenInvalid, response.ErrorType);
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ActivateUserRequestHandler_Handle_Returns_TokenExpired()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        ActivationToken = "testtoken",
                        ActivationTokenExpirationDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1))
                    });
                    context.SaveChanges();
                }

                ActivateUserResponse response;
                var request = new ActivateUserRequest
                {
                    ActivationToken = "testtoken"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new ActivateUserRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Users.Single().IsActive);
                    Assert.AreEqual(ErrorType.TokenExpired, response.ErrorType);
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ActivateUserRequestHandler_Handle_Returns_Success()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        ActivationToken = "token",
                        ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1)
                    });
                    context.SaveChanges();
                }

                ActivateUserResponse response;
                var request = new ActivateUserRequest
                {
                    ActivationToken = "token"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new ActivateUserRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Users.Single().IsActive);
                    Assert.IsNull(context.Users.Single().ActivationToken);
                    Assert.IsTrue(response.Success);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}