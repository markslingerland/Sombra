using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
                var user = new UserKey() { Key = key };
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new CharityActionEntity
                    {
                        CharityActionkey = keyAction,
                        Charitykey = keyCharity,
                        UserKeys = new Collection<UserKey>() { user },
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        NameAction = "",
                        Discription = "0-IBAN",
                        CoverImage = ""

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }

                var userRequest = new Sombra.Messaging.UserKey() { Key = key };
                var request = new GetCharityActionRequest
                {
                    CharityActionkey = keyAction,
                    Charitykey = keyCharity,
                    UserKeys = new Collection<Sombra.Messaging.UserKey>() { userRequest },
                    NameCharity = "testNAmeOwner",
                    Category = Core.Enums.Category.None,
                    IBAN = "",
                    NameAction = "",
                    Discription = "0-IBAN",
                    CoverImage = ""

                };

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
                    
                    Assert.AreEqual(request.CharityActionkey, context.CharityActions.Single().CharityActionkey);
                    Assert.AreEqual(request.Charitykey, context.CharityActions.Single().Charitykey);
                    // TODO Fix unit test problem UserKey in context returns null
                    //Assert.AreEqual(request.UserKeys, context.CharityActions.Single().UserKeys);
                    Assert.AreEqual(request.NameCharity, context.CharityActions.Single().NameCharity);
                    Assert.AreEqual(request.Category, context.CharityActions.Single().Category);
                    Assert.AreEqual(request.IBAN, context.CharityActions.Single().IBAN);
                    Assert.AreEqual(request.NameAction, context.CharityActions.Single().NameAction);
                    Assert.AreEqual(request.Discription, context.CharityActions.Single().Discription);
                    Assert.AreEqual(request.CoverImage, context.CharityActions.Single().CoverImage);
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
                    var charity = new CharityActionEntity
                    {
                        CharityActionkey = keyAction,
                        Charitykey = keyCharity,
                        UserKeys = users,
                        NameCharity = "testNAmeOwner",
                        Category = Core.Enums.Category.None,
                        IBAN = "",
                        NameAction = "",
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