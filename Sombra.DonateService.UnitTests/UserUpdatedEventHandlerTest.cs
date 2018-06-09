using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Shared;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class UserUpdatedEventHandlerTest
    {
        [TestMethod]
        public async Task UserUpdatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                var user = new User{
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var Event = new UserUpdatedEvent(){
                    UserKey = user.UserKey,
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    ProfileImage = "UglyImage",
                    BirthDate = DateTime.UtcNow,
                    City = "TestCity",
                    Country = "TestCountry",
                    EmailAddress = "test@test.nl",
                    FirstName = "FirstTest",
                    LastName = "LastTest", 
                    PhoneNumber = "0600000000"
                };       

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    var handler = new UserUpdatedEventHandler(context);
                    await handler.ConsumeAsync(Event);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.Users.Count());
                    Assert.AreEqual(Event.FirstName + " " + Event.LastName, context.Users.Single().UserName);  
                    Assert.AreEqual(Event.ProfileImage, context.Users.Single().ProfileImage);  
                    Assert.AreEqual(Event.UserKey, context.Users.Single().UserKey);  
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}