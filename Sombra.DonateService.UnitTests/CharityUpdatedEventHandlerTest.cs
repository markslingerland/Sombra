using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class CharityUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityUpdatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity(){
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                };      

                var updatedCharityEvent = new CharityUpdatedEvent(){
                    CharityKey = charity.CharityKey,
                    Name = "Pretty Charity Name",
                    CoverImage = "new CoverImage",
                    ThankYou = "ThankYou"
                };     

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }        
                
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new CharityUpdatedEventHandler(context);
                    await handler.ConsumeAsync(updatedCharityEvent);      
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Charities.Count());
                    Assert.AreEqual(updatedCharityEvent.Name, context.Charities.Single().Name);
                    Assert.AreEqual(updatedCharityEvent.ThankYou, context.Charities.Single().ThankYou);
                    Assert.AreEqual(updatedCharityEvent.CoverImage, context.Charities.Single().Image);

                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}