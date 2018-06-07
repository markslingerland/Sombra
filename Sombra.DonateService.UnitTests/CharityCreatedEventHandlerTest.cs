using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Shared;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class CharityCreatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityCreatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

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

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new CharityCreatedEventHandler(context);
                    await handler.ConsumeAsync(Event);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Charities.Count());
                    Assert.AreEqual(Event.Name, context.Charities.Single().Name); 
                    Assert.AreEqual(Event.ThankYou, context.Charities.Single().ThankYou);  
                    Assert.AreEqual(Event.CoverImage, context.Charities.Single().Image);  
                     
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}