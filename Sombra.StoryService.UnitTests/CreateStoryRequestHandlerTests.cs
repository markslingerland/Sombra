using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.UnitTests
{
    [TestClass]
    public class CreateStoryRequestHandlerTests
    {
        [TestMethod]
        public async Task CreateStoryRequestHandler_Handle_Returns_Success()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var author = new User
                {
                    UserKey = Guid.NewGuid()
                };

                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Users.Add(author);
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                var request = new CreateStoryRequest
                {
                    CharityKey = charity.CharityKey,
                    AuthorUserKey = author.UserKey,
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    CoverImage = "cover",
                    Images = new List<string>
                    {
                        "image1",
                        "image2"
                    }
                };

                CreateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CreateStoryRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.IsSuccess);
                    Assert.AreEqual(1, context.Stories.Count());
                    Assert.AreEqual(request.CoverImage, context.Stories.Single().CoverImage);
                    Assert.AreEqual(request.Title, context.Stories.Single().Title);
                    Assert.AreEqual(request.StoryKey, context.Stories.Single().StoryKey);
                    Assert.AreEqual(request.AuthorUserKey, context.Stories.IncludeAuthor().Single().Author.UserKey);
                    Assert.AreEqual(2, context.Images.Count());
                    Assert.AreEqual(2, context.Stories.IncludeImages().Single().Images.Count);
                    Assert.AreEqual(1, context.Images.Count(i => i.Base64 == "image1"));
                    Assert.AreEqual(1, context.Images.Count(i => i.Base64 == "image2"));
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateStoryRequestHandler_Handle_Returns_CharityNotFound()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var author = new User
                {
                    UserKey = Guid.NewGuid()
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Users.Add(author);
                    context.SaveChanges();
                }

                var request = new CreateStoryRequest
                {
                    CharityKey = Guid.NewGuid(),
                    AuthorUserKey = author.UserKey,
                    StoryKey = Guid.NewGuid(),
                    Title = "title",
                    CoverImage = "cover",
                    Images = new List<string>
                    {
                        "image1",
                        "image2"
                    }
                };

                CreateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CreateStoryRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.AreEqual(ErrorType.CharityNotFound, response.ErrorType);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateStoryRequestHandler_Handle_Returns_InvalidKey()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                }

                var request = new CreateStoryRequest
                {
                    CharityKey = charity.CharityKey,
                    StoryKey = Guid.Empty,
                    Title = "title",
                    CoverImage = "cover",
                    Images = new List<string>
                    {
                        "image1",
                        "image2"
                    }
                };

                CreateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CreateStoryRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsFalse(context.Stories.Any());
                    Assert.IsFalse(context.Images.Any());
                    Assert.AreEqual(ErrorType.InvalidKey, response.ErrorType);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateStoryRequestHandler_Handle_Returns_InvalidUserKey()
        {
            StoryContext.OpenInMemoryConnection();

            try
            {
                var author = new User
                {
                    UserKey = Guid.NewGuid()
                };

                var charity = new Charity
                {
                    CharityKey = Guid.NewGuid()
                };

                using (var context = StoryContext.GetInMemoryContext())
                {
                    context.Users.Add(author);
                    context.Charities.Add(charity);
                    context.SaveChanges();
                }

                var request = new CreateStoryRequest
                {
                    CharityKey = charity.CharityKey,
                    StoryKey = Guid.NewGuid(),
                    AuthorUserKey = Guid.NewGuid(),
                    Title = "title",
                    CoverImage = "cover",
                    Images = new List<string>
                    {
                        "image1",
                        "image2"
                    }
                };

                CreateStoryResponse response;
                using (var context = StoryContext.GetInMemoryContext())
                {
                    var handler = new CreateStoryRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = StoryContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsFalse(context.Stories.Any());
                    Assert.IsFalse(context.Images.Any());
                    Assert.AreEqual(ErrorType.InvalidUserKey, response.ErrorType);
                }
            }
            finally
            {
                StoryContext.CloseInMemoryConnection();
            }
        }
    }
}