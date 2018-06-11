using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.StoryService.DAL;
using Sombra.Messaging.Events.Charity;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class CharityUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityUpdatedEventHandler_Handles_Event()
        {
            StoryContext.OpenInMemoryConnection();
            try
            {
                var charityUpdatedEvent = new CharityUpdatedEvent
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "newName"
                };
                var otherKey = Guid.NewGuid();

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var charity1 = new Charity
                    {
                        CharityKey = charityUpdatedEvent.CharityKey,
                        Name = "oldName"
                    };
                    var charity2 = new Charity
                    {
                        CharityKey = otherKey,
                        Name = "otherOldName"
                    };

                    context.Charities.Add(charity1);
                    context.Charities.Add(charity2);
                    context.SaveChanges();
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(charityUpdatedEvent);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Charities.Count(a => a.CharityKey == charityUpdatedEvent.CharityKey && a.Name == charityUpdatedEvent.Name));
                    Assert.AreEqual(1, context.Charities.Count(a => a.CharityKey == otherKey && a.Name == "otherOldName"));
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}