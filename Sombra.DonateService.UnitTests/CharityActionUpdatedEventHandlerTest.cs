using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.CharityAction;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class CharityActionUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityActionUpdatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                var charity = new DAL.Charity{
                    CharityKey = Guid.NewGuid(),
                    Name = "Test",
                    ChartityActions = new List<CharityAction>(),
                };

                var charityAction = new CharityAction(){
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };   

                var updatedCharityActionEvent = new CharityActionUpdatedEvent(){
                    CharityActionKey = charityAction.CharityActionKey,
                    CoverImage = "pretty image",
                    CharityName = "Pretty Charity Name",
                    Name = "Pretty CharityAction Name",
                    ThankYou = "ThankYouVeryMuch"                    
                };     

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityActions.Add(charityAction);
                    context.SaveChanges();
                }        
                
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new CharityActionUpdatedEventHandler(context);
                    await handler.ConsumeAsync(updatedCharityActionEvent);      
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.CharityActions.Count());
                    Assert.AreEqual(updatedCharityActionEvent.ActionEndDateTime, context.CharityActions.Single().ActionEndDateTime);
                    Assert.AreEqual(updatedCharityActionEvent.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.AreEqual(updatedCharityActionEvent.ThankYou, context.CharityActions.Single().ThankYou);
                    Assert.AreEqual(updatedCharityActionEvent.Name, context.CharityActions.Single().Name);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}