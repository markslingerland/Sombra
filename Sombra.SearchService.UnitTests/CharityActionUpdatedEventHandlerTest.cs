using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events.CharityAction;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.UnitTests
{    
    [TestClass]
    public class CharityActionUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityActionUpdatedEventHandler_Handle_Returns_Success()
        {
            SearchContext.OpenInMemoryConnection();

            try
            {
                var content = new Content(){
                    Category = Core.Enums.Category.EnvironmentAndNatureConservation,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    CharityName = "TestName",
                    CharityActionName = "TestName",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.CharityAction
                };   

                var updatedCharityActionEvent = new CharityActionUpdatedEvent(){
                    CharityActionKey = content.CharityActionKey,
                    CoverImage = "pretty image",
                    CharityName = "Pretty Charity Name",
                    Name = "Pretty CharityAction Name",
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
                    var handler = new CharityActionUpdatedEventHandler(context);
                    await handler.ConsumeAsync(updatedCharityActionEvent);      
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    Assert.AreEqual(content.CharityKey, context.Content.Single().CharityKey);
                    Assert.AreEqual(updatedCharityActionEvent.CoverImage, context.Content.Single().Image);
                    Assert.AreEqual(content.Category, context.Content.Single().Category);
                    Assert.AreEqual(content.Description, context.Content.Single().Description);
                    Assert.AreEqual(Core.Enums.SearchContentType.CharityAction, context.Content.Single().Type);
                    Assert.AreEqual(updatedCharityActionEvent.CharityName, context.Content.Single().CharityName);  
                    Assert.AreEqual(updatedCharityActionEvent.Name, context.Content.Single().CharityActionName);  

                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}