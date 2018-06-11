using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class GetStoryByUrlRequestHandlerTests
    {
        [TestMethod]
        public async Task GetStoryByUrlRequestHandler_Handle_Returns_Story()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    Url = "some"
                };

                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    Charity = charity,
                    UrlComponent = "none"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                GetStoryByUrlResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoryByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoryByUrlRequest
                    {
                        CharityUrl = "some",
                        StoryUrlComponent = "none"
                    });
                }

                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(story.StoryKey, response.Story.StoryKey);
                Assert.AreEqual(story.Title, response.Story.Title);
                Assert.IsNull(response.Story.AuthorUserKey);
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetStoryByUrlRequestHandler_Handle_Returns_No_Story()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid(),
                    Url = "none"
                };

                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    Charity = charity,
                    UrlComponent = "some"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                GetStoryByUrlResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoryByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoryByUrlRequest
                    {
                        CharityUrl = "some",
                        StoryUrlComponent = "none"
                    });
                }

                Assert.IsFalse(response.IsSuccess);
                Assert.IsNull(response.Story);
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}