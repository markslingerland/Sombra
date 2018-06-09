using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events.Charity;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class CharityUpdatedEventHandlerTests
    {
        [TestMethod]
        public async Task CharityUpdatedEventHandler_Consume_Updates_Name()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var story = new Story
                {
                    CharityKey = Guid.NewGuid(),
                    CharityName = "TestName",
                    CoverImage = "image"
                };

                var updatedCharityEvent = new CharityUpdatedEvent
                {
                    CharityKey = story.CharityKey,
                    CoverImage = "pretty image",
                    Name = "Pretty Charity Name"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }        
                
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(updatedCharityEvent);      
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual(story.CharityKey, context.Stories.Single().CharityKey);
                    Assert.AreEqual("image", context.Stories.Single().CoverImage);
                    Assert.AreEqual(updatedCharityEvent.Name, context.Stories.Single().CharityName);  
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CharityUpdatedEventHandler_Consume_Event_Has_No_CharityKey()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var story = new Story
                {
                    CharityKey = Guid.NewGuid(),
                    CharityName = "TestName",
                    CoverImage = "image"
                };

                var updatedCharityEvent = new CharityUpdatedEvent
                {
                    CharityKey = Guid.Empty,
                    CoverImage = "pretty image",
                    Name = "Pretty Charity Name"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(updatedCharityEvent);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual("image", context.Stories.Single().CoverImage);
                    Assert.AreEqual("TestName", context.Stories.Single().CharityName);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}