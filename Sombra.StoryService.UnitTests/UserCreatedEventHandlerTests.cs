using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.User;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class UserCreatedEventHandlerTests
    {
        [TestMethod]
        public async Task UserCreatedEventHandler_Consume_Creates_User()
        {
            StoryContext.OpenInMemoryConnection();
            try
            {
                var userCreatedEvent = new UserCreatedEvent
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "image",
                    FirstName = "John",
                    LastName = "Doe"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new UserCreatedEventHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    await handler.ConsumeAsync(userCreatedEvent);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreEqual(userCreatedEvent.UserKey, context.Users.Single().UserKey);
                    Assert.AreEqual(userCreatedEvent.ProfileImage, context.Users.Single().ProfileImage);
                    Assert.AreEqual(Helpers.GetUserName(userCreatedEvent), context.Users.Single().Name);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}