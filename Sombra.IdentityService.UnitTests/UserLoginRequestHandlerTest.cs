using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Core.Extensions;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class UserLoginRequestHandlerTest
    {
        [TestMethod]
        public async Task UserLoginRequestHandler_Handle_Returns_Success()
        {
            AuthenticationContext.OpenInMemoryConnection();

            try
            {
                //Arrange
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now,
                        IsActive = true,
                        Role = Role.Donator
                    };

                    var credential = new Credential
                    {
                        CredentialType = CredentialType.Default,
                        User = user,
                        Identifier = "Admin",
                        Secret = Encryption.CreateHash("admin"),
                    };

                    context.Add(user);
                    context.Add(credential);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    LoginTypeCode = CredentialType.Default
                };

                //Act
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UserLoginRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.IsSuccess);
                    Assert.IsTrue(Encryption.ValidatePassword("admin", response.EncrytedPassword));
                    Assert.AreEqual(response.UserName, context.Users.Single().Name);
                    Assert.AreEqual(response.UserKey, context.Users.Single().UserKey);
                    Assert.IsTrue(response.Role.OnlyHasFlag(Role.Donator));
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserLoginRequestHandler_Handle_Returns_InactiveAccount()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now,
                        IsActive = false,
                        Role = Role.Donator
                    };

                    var credential = new Credential
                    {
                        CredentialType = CredentialType.Default,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin"),
                    };

                    context.Add(user);
                    context.Add(credential);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    LoginTypeCode = CredentialType.Default
                };

                //Act
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UserLoginRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.AreEqual(ErrorType.InactiveAccount, response.ErrorType);
                Assert.IsNull(response.EncrytedPassword);
                Assert.IsFalse(response.IsSuccess);
                Assert.IsNull(response.UserName);
                Assert.AreEqual(response.UserKey, Guid.Empty);
                Assert.AreEqual(Role.Default, response.Role);
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}