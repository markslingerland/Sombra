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
    public class GetCharityActionByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityActionByKeyRequest_Handle_Returns_CharityAction()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var key = Guid.NewGuid();
                var user = new UserKey { Key = key };
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var charity = new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = new List<UserKey> { user },
                        CharityName = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    };
                    context.UserKeys.Add(user);
                    context.CharityActions.Add(charity);
                    context.SaveChanges();

                }

                var request = new GetCharityActionByKeyRequest
                {
                    CharityActionKey = keyAction
                };

                GetCharityActionByKeyResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {

                    var handler = new GetCharityActionByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    
                    Assert.AreEqual(response.Content.CharityActionKey, context.CharityActions.Single().CharityActionKey);
                    Assert.AreEqual(response.Content.CharityKey, context.CharityActions.Single().CharityKey);
                    CollectionAssert.AreEquivalent(response.Content.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(response.Content.CharityName, context.CharityActions.Single().CharityName);
                    Assert.AreEqual(response.Content.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(response.Content.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(response.Content.Name, context.CharityActions.Single().Name);
                    Assert.AreEqual(response.Content.Description, context.CharityActions.Single().Description);
                    Assert.AreEqual(response.Content.CoverImage, context.CharityActions.Single().CoverImage);
                    Assert.IsTrue(response.Success);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityActionByKeyRequest_Handle_Returns_Null()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var keyAction = Guid.NewGuid();
                    var keyCharity = Guid.NewGuid();
                    var user = new UserKey() { Key = Guid.NewGuid() };
                    var users = new Collection<UserKey>() { user };
                    var charity = new CharityAction
                    {
                        CharityActionKey = keyAction,
                        CharityKey = keyCharity,
                        UserKeys = users,
                        CharityName = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        Name = "",
                        Description = "0-IBAN",
                        CoverImage = ""

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityActionByKeyRequest();

                GetCharityActionByKeyResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert               
                Assert.IsFalse(response.Success);
                
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}