using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class GetStoriesRequestHandlerTests
    {
        [TestMethod]
        public async Task GetStoriesRequestHandlerTests_Handle_Returns_Stories()
        {
            StoryContext.OpenInMemoryConnection();
            try
            {
                using (var context = StoryContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.Stories.Add(new Story
                        {
                            StoryKey = Guid.NewGuid()
                        });
                    }

                    context.SaveChanges();
                }

                GetStoriesResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoriesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoriesRequest
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
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetStoriesRequestHandlerTests_Handle_Returns_Filtered_Stories()
        {
            StoryContext.OpenInMemoryConnection();
            try
            {
                var charityKey1 = Guid.NewGuid();
                var charityKey2 = Guid.NewGuid();
                var authorUserKey = Guid.NewGuid();

                using (var context = StoryContext.GetInMemoryContext())
                {
                    for (var i = 0; i < 25; i++)
                    {
                        context.Stories.Add(new Story
                        {
                            StoryKey = Guid.NewGuid()
                        });
                    }

                    for (var i = 0; i < 10; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (i % 4 == 0)
                            {
                                context.Stories.Add(new Story
                                {
                                    CharityKey = charityKey1,
                                    StoryKey = Guid.NewGuid(),
                                    IsApproved = true,
                                    Author = new User
                                    {
                                        UserKey = authorUserKey,
                                        Name = "John Doe"
                                    }
                                });
                            }
                            else
                            {
                                context.Stories.Add(new Story
                                {
                                    CharityKey = charityKey2,
                                    StoryKey = Guid.NewGuid(),
                                    IsApproved = true
                                });
                            }
                        }
                        else
                        {
                            context.Stories.Add(new Story
                            {
                                CharityKey = charityKey2,
                                StoryKey = Guid.NewGuid(),
                                IsApproved = true
                            });
                        }
                    }

                    context.SaveChanges();
                }

                GetStoriesResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoriesRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoriesRequest
                    {
                        OnlyApproved = true,
                        AuthorUserKey = authorUserKey,
                        PageNumber = 2,
                        PageSize = 2
                    });
                }

                Assert.AreEqual(3, response.TotalNumberOfResults);
                Assert.AreEqual(1, response.Results.Count);
                Assert.IsTrue(response.Results.All(r => r.AuthorUserKey == authorUserKey && r.AuthorName == "John Doe" && r.IsApproved && r.CharityKey == charityKey1));
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}