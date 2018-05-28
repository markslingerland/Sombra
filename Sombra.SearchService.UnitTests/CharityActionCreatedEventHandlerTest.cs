using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events;
using Sombra.Messaging.Shared;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.UnitTests
{
    [TestClass]
    public class CharityActionCreatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityActionCreatedEventHandler_Handle_Returns_Success()
        {
            SearchContext.OpenInMemoryConnection();

            try
            {
                var Event = new CharityActionCreatedEvent
                {
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud,
                    CharityActionKey = Guid.NewGuid(),
                    CoverImage = "No image given",
                    IBAN = "NotReallyAnIBAN",
                    Description = "This is a very good testing description",
                    CharityKey = Guid.NewGuid(),
                    Name = "TestNameAction",
                    CharityName = "TestName",
                    UserKeys = new List<UserKey>{ new UserKey { Key = Guid.NewGuid() } }
                };

                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new CharityActionCreatedEventHandler(context);
                    await handler.ConsumeAsync(Event);
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    Assert.AreEqual(Event.CharityActionKey, context.Content.Single().CharityActionKey);
                    Assert.AreEqual(Event.CharityKey, context.Content.Single().CharityKey);
                    Assert.AreEqual(Event.CoverImage, context.Content.Single().Image);
                    Assert.AreEqual(Event.Category, context.Content.Single().Category);
                    Assert.AreEqual(Event.Description, context.Content.Single().Description);
                    Assert.AreEqual(Core.Enums.SearchContentType.CharityAction, context.Content.Single().Type);
                    Assert.AreEqual(Event.CharityName, context.Content.Single().CharityName);  
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}