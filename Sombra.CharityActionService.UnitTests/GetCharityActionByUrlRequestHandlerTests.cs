using System;
using System.Linq;
using System.Threading.Tasks;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using UserKey = Sombra.CharityActionService.DAL.UserKey;

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class GetCharityActionByUrlRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityActionByUrlRequestHandler_Handle_Returns_CharityAction()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                var keyAction = Guid.NewGuid();
                var charityKey = Guid.NewGuid();
                var key = Guid.NewGuid();
                var user = new UserKey { Key = key };
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = charityKey,
                        Url = "some"
                    };

                    var charityAction = new CharityAction
                    {
                        CharityActionKey = keyAction,
                        UserKeys = new List<UserKey> { user },
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = "",
                        Logo = "test",
                        Charity = charity,
                        UrlComponent = "none"
                    };
                    context.UserKeys.Add(user);
                    context.CharityActions.Add(charityAction);
                    context.SaveChanges();

                }

                var request = new GetCharityActionByUrlRequest
                {
                    CharityUrl = "some",
                    CharityActionUrlComponent = "none"
                };

                GetCharityActionByUrlResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {

                    var handler = new GetCharityActionByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    
                    Assert.AreEqual(response.CharityAction.CharityActionKey, context.CharityActions.Single().CharityActionKey);
                    Assert.AreEqual(response.CharityAction.CharityKey, charityKey);
                    CollectionAssert.AreEquivalent(response.CharityAction.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(response.CharityAction.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(response.CharityAction.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(response.CharityAction.Name, context.CharityActions.Single().Name);
                    Assert.AreEqual(response.CharityAction.Description, context.CharityActions.Single().Description);
                    Assert.AreEqual(response.CharityAction.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.AreEqual(response.CharityAction.Logo, context.CharityActions.Single().Logo);
                    Assert.IsTrue(response.IsSuccess);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionByUrlRequestHandler_Handle_Returns_Null()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var keyAction = Guid.NewGuid();
                    var user = new UserKey { Key = Guid.NewGuid() };
                    var users = new Collection<UserKey> { user };

                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid(),
                        Url = "some"
                    };

                    var charityAction = new CharityAction
                    {
                        CharityActionKey = keyAction,
                        UserKeys = users,
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = "",
                        Logo = "",
                        Charity = charity,
                        UrlComponent = "none"
                    };

                    context.Add(charityAction);
                    context.SaveChanges();

                }
                var request = new GetCharityActionByUrlRequest
                {
                    CharityUrl = "none",
                    CharityActionUrlComponent = "some"
                };

                GetCharityActionByUrlResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.IsFalse(response.IsSuccess);
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}