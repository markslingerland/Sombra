using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Events.Charity;

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

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(charityUpdatedEvent);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Charities.Count(a => a.CharityKey == charityUpdatedEvent.CharityKey && a.Name == charityUpdatedEvent.Name));
                    Assert.AreEqual(1, context.Charities.Count(a => a.CharityKey == otherKey && a.Name == "otherOldName"));
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}