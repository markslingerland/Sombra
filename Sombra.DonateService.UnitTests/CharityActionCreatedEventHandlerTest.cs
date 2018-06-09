using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Shared;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class CharityActionCreatedEventHandlerTest
    {
        [TestMethod]
        public async Task CharityActionCreatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                var Charity = new DAL.Charity{
                    	CharityKey = Guid.NewGuid(),
                        Name = "Test",
                        ChartityActions = new List<DAL.CharityAction>(),
                };

                var Event = new CharityActionCreatedEvent
                {
                    Category = Core.Enums.Category.EnvironmentAndNatureConservation,
                    CharityActionKey = Guid.NewGuid(),
                    CoverImage = "No image given",
                    IBAN = "NotReallyAnIBAN",
                    Description = "This is a very good testing description",
                    CharityKey = Charity.CharityKey,
                    Name = "TestNameAction",
                    CharityName = Charity.Name,
                    UserKeys = new List<UserKey>{ new UserKey { Key = Guid.NewGuid() } },
                    ThankYou = "ThankYou"
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Charities.Add(Charity);
                    await context.SaveChangesAsync();
                    var handler = new CharityActionCreatedEventHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    await handler.ConsumeAsync(Event);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.CharityActions.Count());
                    Assert.AreEqual(Event.CharityActionKey, context.CharityActions.Single().CharityActionKey);
                    Assert.AreEqual(Event.ActionEndDateTime, context.CharityActions.Single().ActionEndDateTime);
                    Assert.AreEqual(Event.ThankYou, context.CharityActions.Single().ThankYou);
                    Assert.AreEqual(Event.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.AreEqual(Event.Name, context.CharityActions.Single().Name);  
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}