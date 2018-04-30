using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Events;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Updates_Name()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<AuthenticationContext>()
                    .UseSqlite(connection)
                    .Options;

                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe",
                    EmailAddress = "ellen@doe.com"
                };

                var credentialType = new CredentialType
                {
                    Name = Core.Enums.CredentialType.Email
                };

                var user = new User
                {
                    UserKey = userUpdatedEvent.UserKey,
                    Name = "John Doe"
                };

                var credential = new Credential
                {
                    CredentialType = credentialType,
                    User = user,
                    Identifier = "john@doe.com"
                };

                using (var context = new AuthenticationContext(options, false))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(user);
                    context.CredentialTypes.Add(credentialType);
                    context.Credentials.Add(credential);

                    context.SaveChanges();
                }

                using (var context = new AuthenticationContext(options, false))
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.Consume(userUpdatedEvent);
                }

                using (var context = new AuthenticationContext(options, false))
                {
                    Assert.AreEqual("Ellen Doe", context.Users.Single().Name);
                    Assert.AreEqual("ellen@doe.com", context.Credentials.Single().Identifier);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Event_Has_No_UserKey()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<AuthenticationContext>()
                    .UseSqlite(connection)
                    .Options;

                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.Empty,
                    FirstName = "Ellen",
                    LastName = "Doe"
                };

                var credentialType = new CredentialType
                {
                    Name = Core.Enums.CredentialType.Email
                };

                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    Name = "John Doe"
                };

                var credential = new Credential
                {
                    CredentialType = credentialType,
                    User = user,
                    Identifier = "john@doe.com"
                };

                using (var context = new AuthenticationContext(options, false))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(user);
                    context.CredentialTypes.Add(credentialType);
                    context.Credentials.Add(credential);

                    context.SaveChanges();
                }

                using (var context = new AuthenticationContext(options, false))
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.Consume(userUpdatedEvent);
                }

                using (var context = new AuthenticationContext(options, false))
                {
                    Assert.AreEqual("John Doe", context.Users.Single().Name);
                    Assert.AreEqual("john@doe.com", context.Credentials.Single().Identifier);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}