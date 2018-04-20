using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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


            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<AuthenticationContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new AuthenticationContext(options))
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
                using (var context = new AuthenticationContext(options))
                {
                    var handler = new UserLoginRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new AuthenticationContext(options))
                {
                    Assert.IsTrue(response.Success);
                    Assert.AreEqual(response.UserName, context.Users.Single().Name);
                    Assert.AreEqual(response.UserKey, context.Users.Single().UserKey);
                    CollectionAssert.AreEqual(response.PermissionCodes, context.Permissions.Select(b => b.Name.ToString()).ToList());
                }
<<<<<<< HEAD
=======



>>>>>>> master
            }
            finally
            {
                connection.Close();
            }



        }

        [TestMethod]
        public async Task Handle_WrongPassword()
        {


            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<AuthenticationContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new AuthenticationContext(options))
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
                using (var context = new AuthenticationContext(options))
                {
                    var handler = new UserLoginRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new AuthenticationContext(options))
                {
                    Assert.IsFalse(response.Success);
                    Assert.AreEqual(response.UserName, null);
                    Assert.AreEqual(response.UserKey, Guid.Empty);
                    CollectionAssert.AreEqual(response.PermissionCodes, null);
                }

            }
            finally
            {
                connection.Close();
            }

        }

    }
}