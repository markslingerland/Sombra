using System;
using System.Collections.Generic;
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
    public class GetCharityActionsRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityActionsRequestHandler_Handle_Returns_CharityActions()
        {
            DonationsContext.OpenInMemoryConnection();
            try
            {
                var charity1 = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    ChartityActions = new List<CharityAction>()
                };

                var charity2 = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    ChartityActions = new List<CharityAction>()
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        charity1.ChartityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid(),
                            Name = "CharityAction"
                        });
                    }

                    for (var i = 0; i < 20; i++)
                    {
                        charity2.ChartityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid(),
                            Name = "CharityAction"
                        });
                    }
                    context.AddRange(charity1, charity2);
                    context.SaveChanges();
                }

                GetCharityActionsResponse response;
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionsRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharityActionsRequest
                    {
                        CharityKey = charity1.CharityKey
                    });
                }

                Assert.AreEqual(25, response.CharityActions.Count);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionsRequestHandler_Handle_Returns_No_CharityActions()
        {
            DonationsContext.OpenInMemoryConnection();
            try
            {
                var charity1 = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    ChartityActions = new List<CharityAction>()
                };

                var charity2 = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    ChartityActions = new List<CharityAction>()
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        charity1.ChartityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid(),
                            Name = "CharityAction"
                        });
                    }

                    for (var i = 0; i < 20; i++)
                    {
                        charity2.ChartityActions.Add(new CharityAction
                        {
                            CharityActionKey = Guid.NewGuid(),
                            Name = "CharityAction"
                        });
                    }
                    context.AddRange(charity1, charity2);
                    context.SaveChanges();
                }

                GetCharityActionsResponse response;
                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionsRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharityActionsRequest
                    {
                        CharityKey = Guid.NewGuid()
                    });
                }

                Assert.AreEqual(0, response.CharityActions.Count);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}