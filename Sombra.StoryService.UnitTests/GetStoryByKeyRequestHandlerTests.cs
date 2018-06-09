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
    public class GetStoryByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task GetStoryByKeyRequestHandler_Handle_Returns_Story()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                GetStoryByKeyResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoryByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoryByKeyRequest
                    {
                        StoryKey = story.StoryKey
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
        public async Task GetStoryByKeyRequestHandler_Handle_Returns_No_Story()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title"
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                GetStoryByKeyResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new GetStoryByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(new GetStoryByKeyRequest
                    {
                        StoryKey = Guid.NewGuid()
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