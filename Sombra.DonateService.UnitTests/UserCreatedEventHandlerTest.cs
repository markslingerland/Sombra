using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Events.User;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class UserCreatedEventHandlerTest
    {
        [TestMethod]
        public async Task UserCreatedEventHandler_Handle_Returns_Success()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                var Event = new UserCreatedEvent(){
                    UserKey = Guid.NewGuid(),
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    BirthDate = DateTime.UtcNow,
                    City = "TestCity",
                    Country = "TestCountry",
                    EmailAddress = "test@test.nl",
                    FirstName = "FirstTest",
                    LastName = "LastTest", 
                    PhoneNumber = "0600000000",
                    ProfileImage = "PrettyImage"
                };       

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new UserCreatedEventHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
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