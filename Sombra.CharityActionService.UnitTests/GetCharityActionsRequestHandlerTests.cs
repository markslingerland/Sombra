using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using Sombra.Core.Enums;
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

                Assert.AreEqual(25, response.TotalNumberOfResults);
                Assert.AreEqual(5, response.Results.Count);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionsRequestHandlerTests_Handle_Returns_Active_CharityActions()
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
                            ActionEndDateTime = DateTime.UtcNow - TimeSpan.FromDays(10),
                            CharityActionKey = Guid.NewGuid()
                        });
                    }

                    for (var i = 0; i < 15; i++)
                    {
                        context.CharityActions.Add(new CharityAction
                        {
                            ActionEndDateTime = DateTime.UtcNow + TimeSpan.FromDays(10),
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
                        OnlyActive = true,
                        PageNumber = 2,
                        PageSize = 10
                    });
                }

                Assert.AreEqual(15, response.TotalResult);
                Assert.AreEqual(5, response.Results.Count);
                Assert.IsTrue(response.Results.All(r => r.ActionEndDateTime > DateTime.UtcNow));
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
                            CharityActionKey = Guid.NewGuid(),
                            Name = "this is a charity for john",
                            Description = "doe",
                            Category = Category.EducationAndResearch | Category.Culture
                        });
                    }

                    for (var i = 0; i < 15; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (i % 4 == 0)
                            {
                                context.CharityActions.Add(new CharityAction
                                {
                                    CharityKey = charityKey,
                                    CharityActionKey = Guid.NewGuid(),
                                    Name = "this is a charity for john",
                                    Description = "doe",
                                    Category = Category.EducationAndResearch | Category.Culture
                                });
                            }
                            else
                            {
                                context.CharityActions.Add(new CharityAction
                                {
                                    CharityKey = charityKey,
                                    CharityActionKey = Guid.NewGuid(),
                                    Name = "this is a charity for john",
                                    Description = "doe",
                                    Category = Category.EducationAndResearch | Category.Health
                                });
                            }
                        }
                        else
                        {
                            context.CharityActions.Add(new CharityAction
                            {
                                CharityKey = charityKey,
                                CharityActionKey = Guid.NewGuid()
                            });
                        }
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
                        Category = Category.EducationAndResearch | Category.Culture,
                        Keywords = new List<string> { "john", "doe" },
                        PageNumber = 2,
                        PageSize = 1
                    });
                }

                Assert.AreEqual(4, response.TotalNumberOfResults);
                Assert.AreEqual(1, response.Results.Count);
                Assert.IsTrue(response.Results.All(r => r.CharityKey == charityKey && r.Category == (Category.EducationAndResearch | Category.Culture)));
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}