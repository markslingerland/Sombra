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
    public class GetUserActivationTokenRequestHandlerTest
    {
        [TestMethod]
        public async Task GetUserActivationTokenRequestHandler_Handle_ReturnsToken()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        IsActive = false
                    };
                    var credential = new Credential
                    {
                        Id = Guid.NewGuid(),
                        CredentialType = CredentialType.Email,
                        Identifier = "john@doe.com",
                        User = user
                    };

                    context.Users.Add(user);
                    context.Credentials.Add(credential);
                    context.SaveChanges();
                }

                GetUserActivationTokenResponse response;
                var request = new GetUserActivationTokenRequest
                {
                    EmailAddress = "john@doe.com"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new GetUserActivationTokenRequestHandler(context);
                    response = await handler.HandleAsync(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Users.Single().IsActive);
                    Assert.IsNotNull(response.ActivationToken);
                    Assert.AreEqual(context.Users.Single().ActivationToken, response.ActivationToken);
                    Assert.AreEqual(context.Users.Single().Name, response.UserName);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetUserActivationTokenRequestHandler_Handle_ReturnsInvalidEmail()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        IsActive = false
                    };
                    var credential = new Credential
                    {
                        Id = Guid.NewGuid(),
                        CredentialType = CredentialType.Email,
                        Identifier = "ellen@doe.com",
                        User = user
                    };

                    context.Users.Add(user);
                    context.Credentials.Add(credential);
                    context.SaveChanges();
                }

                GetUserActivationTokenResponse response;
                var request = new GetUserActivationTokenRequest
                {
                    EmailAddress = "john@doe.com"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new GetUserActivationTokenRequestHandler(context);
                    response = await handler.HandleAsync(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Users.Single().IsActive);
                    Assert.IsNull(response.ActivationToken);
                    Assert.AreEqual(ErrorType.InvalidEmail, response.ErrorType);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetUserActivationTokenRequestHandler_Handle_ReturnsAlreadyActive()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        IsActive = true
                    };
                    var credential = new Credential
                    {
                        Id = Guid.NewGuid(),
                        CredentialType = CredentialType.Email,
                        Identifier = "john@doe.com",
                        User = user
                    };

                    context.Users.Add(user);
                    context.Credentials.Add(credential);
                    context.SaveChanges();
                }

                GetUserActivationTokenResponse response;
                var request = new GetUserActivationTokenRequest
                {
                    EmailAddress = "john@doe.com"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new GetUserActivationTokenRequestHandler(context);
                    response = await handler.HandleAsync(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Users.Single().IsActive);
                    Assert.IsNull(response.ActivationToken);
                    Assert.AreEqual(ErrorType.AlreadyActive, response.ErrorType);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}