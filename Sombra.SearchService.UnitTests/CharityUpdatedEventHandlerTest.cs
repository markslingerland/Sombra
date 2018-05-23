using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.UnitTests
{
    [TestClass]
    public class CharityUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityUpdatedEventHandler_Handle_Returns_Success()
        {
            SearchContext.OpenInMemoryConnection();

            try
            {
                var content = new Content(){
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };   

                var updatedCharityEvent = new CharityUpdatedEvent(){
                    CharityKey = content.CharityKey,
                    CoverImage = "pretty image",
                    Name = "Pretty Charity Name",
                    Category = content.Category,
                    Slogan = content.Description,

                };     

                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Content.Add(content);
                    context.SaveChanges();
                }        
                
                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
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