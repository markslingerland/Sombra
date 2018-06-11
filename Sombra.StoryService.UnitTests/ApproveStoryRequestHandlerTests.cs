using System;
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
    public class ApproveStoryRequestHandlerTests
    {
        [TestMethod]
        public async Task ApproveStoryRequestHandler_Handle_Returns_Success()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Charity = charity
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                ApproveStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new ApproveStoryRequestHandler(context);
                    response = await handler.Handle(new ApproveStoryRequest
                    {
                        StoryKey = story.StoryKey
                    });
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.IsSuccess);
                    Assert.IsTrue(context.Stories.Single().IsApproved);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveStoryRequestHandler_Handle_Returns_AlreadyActive()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    IsApproved = true,
                    Charity = charity
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                ApproveStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new ApproveStoryRequestHandler(context);
                    response = await handler.Handle(new ApproveStoryRequest
                    {
                        StoryKey = story.StoryKey
                    });
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsTrue(context.Stories.Single().IsApproved);
                    Assert.AreEqual(ErrorType.AlreadyActive, response.ErrorType);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task ApproveStoryRequestHandler_Handle_Returns_NotFound()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };
                var story = new Story
                {
                    StoryKey = Guid.NewGuid(),
                    Charity = charity
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Stories.Add(story);
                    context.SaveChanges();
                }

                ApproveStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new ApproveStoryRequestHandler(context);
                    response = await handler.Handle(new ApproveStoryRequest
                    {
                        StoryKey = Guid.NewGuid()
                    });
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsFalse(context.Stories.Single().IsApproved);
                    Assert.AreEqual(ErrorType.NotFound, response.ErrorType);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}