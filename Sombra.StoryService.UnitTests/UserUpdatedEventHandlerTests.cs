using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events.User;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlersTest
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Updates_Name()
        {
            StoryContext.OpenInMemoryConnection();

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

                var user = new User
                {
                    UserKey = userUpdatedEvent.UserKey,
                    Name = "John Doe",
                    ProfileImage = "old image"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual("Ellen Doe", context.Users.Single().Name);
                    Assert.AreEqual("new image", context.Users.Single().ProfileImage);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserUpdatedEventHandler_Consume_Event_Has_No_UserKey()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var userUpdatedEvent = new UserUpdatedEvent
                {
                    UserKey = Guid.Empty,
                    FirstName = "Ellen",
                    LastName = "Doe",
                    ProfileImage = "new image"
                };

                var charityAction = new User
                {
                    UserKey = Guid.NewGuid(),
                    Name = "John Doe",
                    ProfileImage = "old image"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Users.Add(charityAction);
                    context.SaveChanges();
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(userUpdatedEvent);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual("John Doe", context.Users.Single().Name);
                    Assert.AreEqual("old image", context.Users.Single().ProfileImage);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}