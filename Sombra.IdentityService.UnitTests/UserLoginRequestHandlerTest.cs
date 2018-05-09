using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Role = Sombra.IdentityService.DAL.Role;

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
                    context.Database.EnsureCreated();

                    var user = new User
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    var role = new Role
                    {
                        RoleName = Core.Enums.Role.Default,
                        User = user,
                    };

                    var credential = new Credential
                    {
                        CredentialType = CredentialType.Default,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin"),
                    };

                    context.Add(user);
                    context.Add(role);
                    context.Add(credential);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    Secret = "admin",
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
                    Assert.IsTrue(response.Success);
                    Assert.AreEqual(response.UserName, context.Users.Single().Name);
                    Assert.AreEqual(response.UserKey, context.Users.Single().UserKey);
                    CollectionAssert.AreEqual(response.Roles, context.Roles.Select(b => b.RoleName).ToList());
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserLoginRequestHandler_Handle_Returns_WrongPassword()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var user = new User
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    var role = new Role
                    {
                        RoleName = Core.Enums.Role.Default,
                        User = user,
                    };

                    var credential = new Credential
                    {
                        CredentialType = CredentialType.Default,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin"),
                    };

                    context.Add(user);
                    context.Add(role);
                    context.Add(credential);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    Secret = "notAdmin",
                    LoginTypeCode = CredentialType.Default
                };

                //Act
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UserLoginRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.AreEqual(ErrorType.InvalidPassword, response.ErrorType);
                Assert.IsFalse(response.Success);
                Assert.IsNull(response.UserName);
                Assert.AreEqual(response.UserKey, Guid.Empty);
                Assert.IsNull(response.Roles);
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
                    context.Database.EnsureCreated();

                    var user = new User
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now,
                        IsActive = false
                    };

                    var role = new Role
                    {
                        RoleName = Core.Enums.Role.Default,
                        User = user,
                    };

                    var credential = new Credential
                    {
                        CredentialType = CredentialType.Default,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin"),
                    };

                    context.Add(user);
                    context.Add(role);
                    context.Add(credential);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    Secret = "admin",
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
                Assert.IsFalse(response.Success);
                Assert.IsNull(response.UserName);
                Assert.AreEqual(response.UserKey, Guid.Empty);
                Assert.IsNull(response.Roles);
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}