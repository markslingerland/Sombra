using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.SearchService.DAL;
using Sombra.Messaging.Events;

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

                var event = new CharityCreatedEvent();

                
                
                using (var context = SearchContext.GetInMemoryContext())
                {
                    var handler = new CreatedCharityEventHandler(context);
                    await handler.Consume(event);
                    
                }

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Content.Count());
                    
                }

            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
    }
}