using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class GetCharitiesRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharitiesRequestHandler_Handle_Returns_Charities()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                using (var context = CharityActionContext.GetInMemoryContext())
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
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharitiesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharitiesRequest());
                }

                Assert.AreEqual(25, response.Charities.Count);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}