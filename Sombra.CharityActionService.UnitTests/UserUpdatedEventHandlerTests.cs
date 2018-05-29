using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Events;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlersTest
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Updates_Name()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.NewGuid(),
                    FirstName = "Ellen",
                    LastName = "Doe",
                    EmailAddress = "ellen@doe.com",
                    ProfileImage = "new image"
                };

                var charityAction = new CharityAction
                {
                    OrganiserUserKey = userUpdatedEvent.UserKey,
                    OrganiserUserName = "John Doe",
                    OrganiserImage = "old image"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charityAction);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual("Ellen Doe", context.CharityActions.Single().OrganiserUserName);
                    Assert.AreEqual("new image", context.CharityActions.Single().OrganiserImage);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Event_Has_No_UserKey()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.Empty,
                    FirstName = "Ellen",
                    LastName = "Doe",
                    ProfileImage = "new image"
                };

                var charityAction = new CharityAction
                {
                    OrganiserUserKey = Guid.NewGuid(),
                    OrganiserUserName = "John Doe",
                    OrganiserImage = "old image"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charityAction);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual("John Doe", context.CharityActions.Single().OrganiserUserName);
                    Assert.AreEqual("old image", context.CharityActions.Single().OrganiserImage);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}