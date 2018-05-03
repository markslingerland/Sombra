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

                    var user = new User()
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now
                    };

                    var role = new Role()
                    {
                        RoleName = Core.Enums.Role.Default,
                        User = user,

                    };

                    var credential = new Credential()
                    {
                        CredentialType = Core.Enums.CredentialType.Default,
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
                    CollectionAssert.AreEqual(response.Roles, context.Roles.Select(b => b.RoleName).ToList());
                }
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

                    var user = new User()
                    {
                        UserKey = Guid.NewGuid(),
                        Name = "Test User",
                        Created = DateTime.Now
                    };

                    var role = new Role()
                    {
                        RoleName = Core.Enums.Role.Default,
                        User = user,

                    };

                    var credential = new Credential()
                    {
                        CredentialType = Core.Enums.CredentialType.Default,
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
                    Assert.IsNull(response.UserName);
                    Assert.AreEqual(response.UserKey, Guid.Empty);
                    Assert.IsNull(response.Permissions);
                    Assert.IsNull(response.Roles);
                }

            }
            finally
            {
                connection.Close();
            }

        }

    }
}