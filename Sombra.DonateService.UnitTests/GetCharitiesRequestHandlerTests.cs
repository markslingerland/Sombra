using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;
using Charity = Sombra.DonateService.DAL.Charity;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class GetCharitiesRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharitiesRequestHandler_Handle_Returns_Charities()
        {
            DonationsContext.OpenInMemoryConnection();
            try
            {
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.Charities.Add(new Charity
                        {
                            CharityKey = Guid.NewGuid(),
                            Name = "charity"
                        });
                    }
                    context.SaveChanges();
                }

                GetCharitiesResponse response;
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new GetCharitiesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharitiesRequest());
                }

                Assert.AreEqual(25, response.Charities.Count);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}