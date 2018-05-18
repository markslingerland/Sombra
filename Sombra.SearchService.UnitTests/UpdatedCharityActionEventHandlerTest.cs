using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.UnitTests
{    
    [TestClass]
    public class UpdatedCharityActionEventHandlerTest
    {
        [TestMethod]
        public async Task UpdatedCharityEventHandler_Handle_Returns_Success()
        {
            SearchContext.OpenInMemoryConnection();           

            try
            {
                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                var content = new Content(){
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };   

                var updatedCharityEvent = new CharityUpdatedEvent(){
                    CoverImage = "pretty image",
                    Name = "Pretty Charity Name"
                };     

                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Content.Add(content);
                }        
                
                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new UpdatedCharityEventHandler(context);
                    await handler.Consume(updatedCharityEvent);      
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    Assert.AreEqual(content.CharityKey, context.Content.Single().CharityKey);
                    Assert.AreEqual(updatedCharityEvent.CoverImage, context.Content.Single().Image);
                    Assert.AreEqual(content.Category, context.Content.Single().Category);
                    Assert.AreEqual(content.Description, context.Content.Single().Description);
                    Assert.AreEqual(Core.Enums.SearchContentType.Charity, context.Content.Single().Type);
                    Assert.AreEqual(updatedCharityEvent.Name, context.Content.Single().Name);  
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}