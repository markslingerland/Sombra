using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.StoryService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class GetCharitiesRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharitiesRequestHandler_Handle_Returns_Charities()
        {
            StoryContext.OpenInMemoryConnection();
            try
            {
                using (var context = StoryContext.GetInMemoryContext())
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
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetCharitiesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharitiesRequest());
                }

                Assert.AreEqual(25, response.Charities.Count);
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}