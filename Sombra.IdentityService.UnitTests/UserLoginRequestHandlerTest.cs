using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class UserLoginRequestHandlerTest
    {
        [TestMethod]
        public async Task Handle_Success()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var credentialType = new CredentialType()
                    {
                        Name = Core.Enums.CredentialType.Default,
                        Position = 1,
                    };

                    var user = new User()
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now
                    };

                    var permission = new Permission()
                    {
                        Name = Core.Enums.Permission.Default,
                        Position = 1
                    };

                    var role = new Role()
                    {
                        Name = Core.Enums.Role.Default,
                        Position = 1
                    };

                    var credential = new Credential()
                    {
                        CredentialType = credentialType,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin")
                    };

                    var rolePermission = new RolePermission()
                    {
                        Role = role,
                        Permission = permission
                    };

                    var userRole = new UserRole()
                    {
                        User = user,
                        Role = role
                    };

                    context.Add(credentialType);
                    context.Add(user);
                    context.Add(permission);
                    context.Add(role);
                    context.Add(credential);
                    context.Add(rolePermission);
                    context.Add(userRole);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    Secret = "admin",
                    LoginTypeCode = Core.Enums.CredentialType.Default
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
                    CollectionAssert.AreEqual(response.Permissions, context.Permissions.Select(b => b.Name).ToList());
                    CollectionAssert.AreEqual(response.Roles, context.Roles.Select(b => b.Name).ToList());
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task Handle_WrongPassword()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var credentialType = new CredentialType()
                    {
                        Name = Core.Enums.CredentialType.Default,
                        Position = 1,
                    };

                    var user = new User()
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now
                    };

                    var permission = new Permission()
                    {
                        Name = Core.Enums.Permission.Default,
                        Position = 1
                    };

                    var role = new Role()
                    {
                        Name = Core.Enums.Role.Default,
                        Position = 1
                    };

                    var credential = new Credential()
                    {
                        CredentialType = credentialType,
                        User = user,
                        Identifier = "Admin",
                        Secret = Core.Encryption.CreateHash("admin")
                    };

                    var rolePermission = new RolePermission()
                    {
                        Role = role,
                        Permission = permission
                    };

                    var userRole = new UserRole()
                    {
                        User = user,
                        Role = role
                    };

                    context.Add(credentialType);
                    context.Add(user);
                    context.Add(permission);
                    context.Add(role);
                    context.Add(credential);
                    context.Add(rolePermission);
                    context.Add(userRole);
                    context.SaveChanges();
                }

                UserLoginResponse response;
                var request = new UserLoginRequest
                {
                    Identifier = "admin",
                    Secret = "notAdmin",
                    LoginTypeCode = Core.Enums.CredentialType.Default
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
                    Assert.IsFalse(response.Success);
                    Assert.IsNull(response.UserName);
                    Assert.AreEqual(response.UserKey, Guid.Empty);
                    Assert.IsNull(response.Permissions);
                    Assert.IsNull(response.Roles);
                }

            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }

        }

    }
}