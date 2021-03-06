﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Events.User;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Updates_Name()
        {
            AuthenticationContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe",
                    EmailAddress = "ellen@doe.com"
                };

                var user = new User
                {
                    UserKey = userUpdatedEvent.UserKey,
                    Name = "John Doe"
                };

                var credential = new Credential
                {
                    CredentialType = CredentialType.Email,
                    User = user,
                    Identifier = "john@doe.com"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.Credentials.Add(credential);

                    context.SaveChanges();
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.AreEqual("Ellen Doe", context.Users.Single().Name);
                    Assert.AreEqual("ellen@doe.com", context.Credentials.Single().Identifier);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Event_Has_No_UserKey()
        {
            AuthenticationContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.Empty,
                    FirstName = "Ellen",
                    LastName = "Doe"
                };

                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    Name = "John Doe"
                };

                var credential = new Credential
                {
                    CredentialType = CredentialType.Email,
                    User = user,
                    Identifier = "john@doe.com"
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.Credentials.Add(credential);

                    context.SaveChanges();
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.AreEqual("John Doe", context.Users.Single().Name);
                    Assert.AreEqual("john@doe.com", context.Credentials.Single().Identifier);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}