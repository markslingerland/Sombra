using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.StoryService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.Charity;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class CharityCreatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityCreatedEventHandler_Handle_Returns_Success()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var Event = new CharityCreatedEvent(){
                    Category = Core.Enums.Category.EnvironmentAndNatureConservation,
                    CharityKey = Guid.NewGuid(),
                    CoverImage = "No image given",
                    Email = "test@test.nl",
                    IBAN = "NotReallyAnIBAN",
                    KVKNumber = "10",
                    Name = "TestName",
                    Url = "test",
                    OwnerUserName = "TestOwnerName",
                    Slogan = "This is a very good testing slogan",
                    ThankYou = "ThankYou"
                };       

                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CharityCreatedEventHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    await handler.ConsumeAsync(Event);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Charities.Count());
                    Assert.AreEqual(Event.Name, context.Charities.Single().Name);
                    Assert.AreEqual(Event.Url, context.Charities.Single().Url);
                     
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}