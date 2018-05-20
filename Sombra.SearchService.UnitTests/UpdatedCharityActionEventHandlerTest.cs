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
        public async Task UpdatedCharityActionEventHandler_Handle_Returns_Success()
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
                    Type = Core.Enums.SearchContentType.CharityAction
                };   

                var updatedCharityActionEvent = new CharityActionUpdatedEvent(){
                    CharityActionKey = content.CharityActionKey,
                    CoverImage = "pretty image",
                    NameAction = "Pretty Charity Name",
                    Category = content.Category,
                    Description = content.Description,
                };     

                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Content.Add(content);
                    context.SaveChanges();
                }        
                
                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new UpdatedCharityActionEventHandler(context);
                    await handler.Consume(updatedCharityActionEvent);      
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    Assert.AreEqual(content.CharityKey, context.Content.Single().CharityKey);
                    Assert.AreEqual(updatedCharityActionEvent.CoverImage, context.Content.Single().Image);
                    Assert.AreEqual(content.Category, context.Content.Single().Category);
                    Assert.AreEqual(content.Description, context.Content.Single().Description);
                    Assert.AreEqual(Core.Enums.SearchContentType.CharityAction, context.Content.Single().Type);
                    Assert.AreEqual(updatedCharityActionEvent.NameAction, context.Content.Single().Name);  
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}