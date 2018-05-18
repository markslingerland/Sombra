using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Sombra.Infrastructure;

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
                    context.Database.EnsureCreated();

                    var charity = new CharityAction
                    {
                        CharityActionkey = keyAction,
                        Charitykey = keyCharity,
                        UserKeys = new List<UserKey> { user },
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        NameAction = "",
                        ActionType = "",
                        Discription = "0-IBAN",
                        CoverImage = ""

                    };
                    context.UserKeys.Add(user);
                    context.CharityActions.Add(charity);
                    context.SaveChanges();

                }

                var userRequest = new Sombra.Messaging.UserKey() { Key = key };
                var request = new GetCharityActionRequest
                {
                    CharityActionkey = keyAction
                };

                GetCharityActionResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {

                    var handler = new GetCharityActionByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    
                    Assert.AreEqual(response.CharityActionkey, context.CharityActions.Single().CharityActionkey);
                    Assert.AreEqual(response.Charitykey, context.CharityActions.Single().Charitykey);
                    CollectionAssert.AreEquivalent(response.UserKeys.Select(k => k.Key).ToList(), context.UserKeys.Select(u => u.Key).ToList());
                    Assert.AreEqual(response.NameCharity, context.CharityActions.Single().NameCharity);
                    Assert.AreEqual(response.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(response.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(response.NameAction, context.CharityActions.Single().NameAction);
                    Assert.AreEqual(response.ActionType, context.CharityActions.Single().ActionType);
                    Assert.AreEqual(response.Discription, context.CharityActions.Single().Discription);
                    Assert.AreEqual(response.CoverImage, context.CharityActions.Single().CoverImage);
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
                    context.Database.EnsureCreated();
                    var keyAction = Guid.NewGuid();
                    var keyCharity = Guid.NewGuid();
                    var user = new UserKey() { Key = Guid.NewGuid() };
                    var users = new Collection<UserKey>() { user };
                    var charity = new CharityAction
                    {
                        CharityActionkey = keyAction,
                        Charitykey = keyCharity,
                        UserKeys = users,
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        NameAction = "",
                        ActionType = "",
                        Discription = "0-IBAN",
                        CoverImage = ""

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityActionRequest();

                GetCharityActionResponse response;

                //Act
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    var handler = new GetCharityActionByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                CharityActionContext.CloseInMemoryConnection();
            }
        }
    }
}