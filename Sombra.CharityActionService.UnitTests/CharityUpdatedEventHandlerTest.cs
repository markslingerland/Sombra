using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Events;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class CharityUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityUpdatedEventHandler_Handles_Event()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                var charityUpdatedEvent = new CharityUpdatedEvent
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "newName"
                };
                var otherKey = Guid.NewGuid();

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var action1 = new CharityAction
                    {
                        CharityKey = charityUpdatedEvent.CharityKey,
                        CharityName = "oldName"
                    };
                    var action2 = new CharityAction
                    {
                        CharityKey = charityUpdatedEvent.CharityKey,
                        CharityName = "oldName"
                    };
                    var action3 = new CharityAction
                    {
                        CharityKey = charityUpdatedEvent.CharityKey,
                        CharityName = "oldName"
                    };
                    var action4 = new CharityAction
                    {
                        CharityKey = otherKey,
                        CharityName = "otherOldName"
                    };

                    context.CharityActions.Add(action1);
                    context.CharityActions.Add(action2);
                    context.CharityActions.Add(action3);
                    context.CharityActions.Add(action4);
                    context.SaveChanges();
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(charityUpdatedEvent);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.CharityActions.Count(a => a.CharityKey == charityUpdatedEvent.CharityKey && a.CharityName == charityUpdatedEvent.Name) == 3);
                    Assert.IsTrue(context.CharityActions.Count(a => a.CharityKey == otherKey && a.CharityName == "otherOldName") == 1);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}