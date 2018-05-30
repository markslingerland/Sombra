using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class GetCharityActionsRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityActionsRequestHandlerTests_Handle_Returns_CharityActions()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.CharityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid()
                        });
                    }

                    context.SaveChanges();
                }

                GetCharityActionsResponse response;
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionsRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharityActionsRequest
                    {
                        PageNumber = 2,
                        PageSize = 20
                    });
                }

                Assert.AreEqual(25, response.TotalResult);
                Assert.AreEqual(5, response.Results.Count);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionsRequestHandlerTests_Handle_Returns_Filtered_CharityActions()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                var charityKey = Guid.NewGuid();
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.CharityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid()
                        });
                    }

                    for (var i = 0; i < 15; i++)
                    {
                        context.CharityActions.Add(new CharityAction
                        {
                            CharityKey = charityKey,
                            CharityActionKey = Guid.NewGuid()
                        });
                    }

                    context.SaveChanges();
                }

                GetCharityActionsResponse response;
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionsRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharityActionsRequest
                    {
                        CharityKey = charityKey,
                        PageNumber = 2,
                        PageSize = 9
                    });
                }

                Assert.AreEqual(15, response.TotalResult);
                Assert.AreEqual(6, response.Results.Count);
                Assert.IsTrue(response.Results.All(r => r.CharityKey == charityKey));
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}