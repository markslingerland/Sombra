using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;
using System;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using UserKey = Sombra.Messaging.Shared.UserKey;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class CreateCharityActionRequestHandlerTests
    {
        [TestMethod]
        public async Task CreateCharityActionRequest_Handle_Returns_Success()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var userRequest = new UserKey { Key = Guid.NewGuid() };
                var request = new CreateCharityActionRequest
                {
                    CharityActionKey = keyAction,
                    CharityKey = keyCharity,
                    UserKeys = new Collection<UserKey> { userRequest },
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    Description = "0-IBAN",
                    CoverImage = "",
                    Logo = "Test"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Charities.Add(new Charity
                    {
                        CharityKey = keyCharity
                    });
                    context.SaveChanges();
                }

                CreateCharityActionResponse response;
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityActionKey, context.CharityActions.Single().CharityActionKey);
                    Assert.AreEqual(request.CharityKey, context.CharityActions.Include(c => c.Charity).Single().Charity.CharityKey);
                    CollectionAssert.AreEquivalent(request.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(request.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(request.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(request.Name, context.CharityActions.Single().Name);
                    Assert.AreEqual(request.Description, context.CharityActions.Single().Description);
                    Assert.AreEqual(request.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.AreEqual(request.Logo, context.CharityActions.Single().Logo);
                    Assert.IsFalse(context.CharityActions.Single().IsApproved);
                    Assert.IsTrue(response.IsSuccess);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateCharityActionRequest_Handle_Returns_InvalidKey()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var keyCharity = Guid.NewGuid();
                var userRequest = new UserKey { Key = Guid.NewGuid() };
                var request = new CreateCharityActionRequest
                {
                    CharityActionKey = Guid.Empty,
                    CharityKey = keyCharity,
                    UserKeys = new Collection<UserKey> { userRequest },
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    Description = "0-IBAN",
                    CoverImage = "",
                    Logo = "test"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Charities.Add(new Charity
                    {
                        CharityKey = keyCharity
                    });
                    context.SaveChanges();
                }

                CreateCharityActionResponse response;
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(ErrorType.InvalidKey, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsFalse(context.CharityActions.Any());
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateCharityActionRequest_Handle_Returns_InvalidCharity()
        {
            CharityActionContext.OpenInMemoryConnection();

            try
            {
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var userRequest = new UserKey { Key = Guid.NewGuid() };
                var request = new CreateCharityActionRequest
                {
                    CharityActionKey = keyAction,
                    CharityKey = keyCharity,
                    UserKeys = new Collection<UserKey> { userRequest },
                    Category = Category.None,
                    IBAN = "",
                    Name = "",
                    Description = "0-IBAN",
                    CoverImage = "",
                    Logo = "Test"
                };

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Charities.Add(new Charity
                    {
                        CharityKey = Guid.NewGuid()
                    });
                    context.SaveChanges();
                }

                CreateCharityActionResponse response;
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityActionRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.AreEqual(ErrorType.CharityNotFound, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                    Assert.IsFalse(context.CharityActions.Any());
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}