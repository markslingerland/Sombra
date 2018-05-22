using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityService.DAL;
using Sombra.Messaging.Events;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlerTests
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Updates_Name()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe",
                    EmailAddress = "ellen@doe.com"
                };

                var charity = new Charity
                {
                    OwnerUserKey = userUpdatedEvent.UserKey,
                    OwnerUserName = "John Doe"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.Consume(userUpdatedEvent);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual("Ellen Doe", context.Charities.Single().OwnerUserName);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Event_Has_No_UserKey()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.Empty,
                    FirstName = "Ellen",
                    LastName = "Doe"
                };

                var charity = new Charity
                {
                    OwnerUserKey = Guid.NewGuid(),
                    OwnerUserName = "John Doe"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.Consume(userUpdatedEvent);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual("John Doe", context.Charities.Single().OwnerUserName);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}