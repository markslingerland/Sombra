using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Role = Sombra.Core.Enums.Role;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class CreateIdentityRequestHandlerTest
    {
        [TestMethod]
        public async Task CreateIdentityRequestHandler_Handle_Returns_ActivationCode()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                CreateIdentityResponse response;
                var request = new CreateIdentityRequest
                {
                    CredentialType = CredentialType.Email,
                    Identifier = "john@doe.com",
                    Role = Role.Donator,
                    Secret = "password",
                    UserKey = Guid.NewGuid(),
                    UserName = "John Doe"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new CreateIdentityRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsNotNull(response.ActivationToken);
                    Assert.IsTrue(response.Success);
                    Assert.IsTrue(context.Users.Count() == 1);
                    Assert.AreEqual(request.UserName, context.Users.Single().Name);
                    Assert.AreEqual(request.UserKey, context.Users.Single().UserKey);
                    Assert.AreEqual(request.Role, context.Users.Single().Role);

                    Assert.IsTrue(context.Credentials.Count() == 1);
                    Assert.AreEqual(request.CredentialType, context.Credentials.Single().CredentialType);
                    Assert.AreEqual(request.Identifier, context.Credentials.Single().Identifier);
                    Assert.AreEqual(request.Secret, context.Credentials.Single().Secret);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateIdentityRequestHandler_Handle_Returns_InvalidUserKey()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                CreateIdentityResponse response;
                var request = new CreateIdentityRequest();

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new CreateIdentityRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Users.Any());
                    Assert.IsNull(response.ActivationToken);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.InvalidKey, response.ErrorType);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateIdentityRequestHandler_Handle_Returns_EmailExists()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe"
                    };
                    context.Credentials.Add(new Credential
                    {
                        Id = Guid.NewGuid(),
                        CredentialType = CredentialType.Email,
                        Identifier = "john@doe.com",
                        User = user
                    });
                    context.Users.Add(user);

                    context.SaveChanges();
                }

                CreateIdentityResponse response;
                var request = new CreateIdentityRequest
                {
                    UserKey = Guid.NewGuid(),
                    Identifier = "john@doe.com",
                    CredentialType = CredentialType.Email
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new CreateIdentityRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Users.Count() == 1);
                    Assert.IsNull(response.ActivationToken);
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(ErrorType.EmailExists, response.ErrorType);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}