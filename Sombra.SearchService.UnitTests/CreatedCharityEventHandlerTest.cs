using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.SearchService.DAL;
using Sombra.Messaging.Events;
using System;

namespace Sombra.SearchService.UnitTests
{
    [TestClass]
    public class CreatedCharityEventHandlerTest
    {
        [TestMethod]
        public async Task CreatedCharityEventHandler_Handle_Returns_Success()
        {
            SearchContext.OpenInMemoryConnection();           

            try
            {
                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                var Event = new CharityCreatedEvent(){
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud,
                    CharityKey = Guid.NewGuid(),
                    CoverImage = "No image given",
                    Email = "test@test.nl",
                    IBAN = "NotReallyAnIBAN",
                    KVKNumber = 10,
                    Name = "TestName",
                    OwnerUserKey = Guid.NewGuid(),
                    OwnerUserName = "TestOwnerName",
                    Slogan = "This is a very good testing slogan",
                };                
                
                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new CreatedCharityEventHandler(context);
                    await handler.Consume(Event);
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    Assert.AreEqual(Event.CharityKey, context.Content.Single().CharityKey);
                    Assert.AreEqual(Event.CoverImage, context.Content.Single().Image);
                    Assert.AreEqual(Event.Category, context.Content.Single().Category);
                    Assert.AreEqual(Event.Slogan, context.Content.Single().Description);
                    Assert.AreEqual(Core.Enums.SearchContentType.Charity, context.Content.Single().Type);
                    Assert.AreEqual(Event.Name, context.Content.Single().Name);  
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}

    