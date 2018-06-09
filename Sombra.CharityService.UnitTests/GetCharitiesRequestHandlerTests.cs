using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityService.DAL;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class GetCharitiesRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharitiesRequestHandlerTests_Handle_Returns_Charities()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                using (var context = CharityContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.Charities.Add(new Charity
                        {
                            CharityKey = Guid.NewGuid()
                        });
                    }

                    context.SaveChanges();
                }

                GetCharitiesResponse response;
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharitiesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharitiesRequest
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
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionsRequestHandlerTests_Handle_Returns_Filtered_Charities()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                var charityKey = Guid.NewGuid();
                using (var context = CharityContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.Charities.Add(new Charity
                        {
                            CharityKey = Guid.NewGuid(),
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
                                context.Charities.Add(new Charity
                                {
                                    CharityKey = charityKey,
                                    Name = "this is a charity for john",
                                    Description = "doe",
                                    Category = Category.EducationAndResearch | Category.Culture
                                });
                            }
                            else
                            {
                                context.Charities.Add(new Charity
                                {
                                    CharityKey = charityKey,
                                    Name = "this is a charity for john",
                                    Description = "doe",
                                    Category = Category.EducationAndResearch | Category.Health
                                });
                            }
                        }
                        else
                        {
                            context.Charities.Add(new Charity
                            {
                                CharityKey = charityKey
                            });
                        }
                    }

                    context.SaveChanges();
                }

                GetCharitiesResponse response;
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharitiesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetCharitiesRequest
                    {
                        Category = Category.EducationAndResearch | Category.Culture,
                        Keywords = new List<string> { "john", "doe" },
                        PageNumber = 3,
                        PageSize = 10
                    });
                }

                Assert.AreEqual(29, response.TotalNumberOfResults);
                Assert.AreEqual(9, response.Results.Count);
                Assert.IsTrue(response.Results.All(r => r.Category == (Category.EducationAndResearch | Category.Culture)));
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}