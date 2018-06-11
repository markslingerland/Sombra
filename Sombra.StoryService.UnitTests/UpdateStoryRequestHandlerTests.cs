using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class UpdateStoryRequestHandlerTests
    {
        [TestMethod]
        public async Task UpdateStoryRequestHandler_Handle_Returns_Success()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };
                var image1 = new Image
                {
                    Base64 = "image1"
                };

                var image2 = new Image
                {
                    Base64 = "image2"
                };
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    CoverImage = "cover",
                    Charity = charity,
                    Images = new List<Image>
                    {
                        image1,
                        image2
                    }
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                var request = new UpdateStoryRequest
                {
                    StoryKey = story.StoryKey,
                    Title = "newTitle",
                    Images = new List<string>
                    {
                        "image3"
                    },
                    StoryImage = "storyImage"
                };

               UpdateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new UpdateStoryRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.IsSuccess);
                    Assert.AreEqual(request.StoryImage, context.Stories.Single().StoryImage);
                    Assert.AreEqual(request.Title, context.Stories.Single().Title);
                    Assert.AreEqual(1, context.Images.Count());
                    Assert.AreEqual(request.Images.First(), context.Images.Single().Base64);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UpdateStoryRequestHandler_Handle_Returns_NotFound()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };
                var image1 = new Image
                {
                    Base64 = "image1"
                };

                var image2 = new Image
                {
                    Base64 = "image2"
                };
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    CoverImage = "cover",
                    Charity = charity,
                    Images = new List<Image>
                    {
                        image1,
                        image2
                    }
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                var request = new UpdateStoryRequest
                {
                    StoryKey = Guid.NewGuid(),
                    Title = "newTitle",
                    Images = new List<string>
                    {
                        "image3"
                    },
                    StoryImage = "storyImage"
                };

                UpdateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new UpdateStoryRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                    Assert.AreEqual(story.StoryImage, context.Stories.Single().StoryImage);
                    Assert.AreEqual(story.Title, context.Stories.Single().Title);
                    Assert.AreEqual(story.CoverImage, story.CoverImage);
                    Assert.AreEqual(story.CoreText, story.CoreText);
                    Assert.AreEqual(2, context.Images.Count());
                    Assert.AreEqual(1, context.Images.Count(i => i.Base64 == image1.Base64));
                    Assert.AreEqual(1, context.Images.Count(i => i.Base64 == image2.Base64));
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}