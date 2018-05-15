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

namespace Sombra.CharityActionService.UnitTests
{
    [TestClass]
    public class GetCharityActionByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityActionByKeyRequest_Handle_Returns_Charity()
        {
            CharityActionContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                var keyAction = Guid.NewGuid();
                var keyCharity = Guid.NewGuid();
                var users = new Collection<Guid>();
                using (var context = CharityActionContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

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
                var request = new GetCharityActionRequest()
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
                    // TODO Fix unit test problem
                    Assert.AreEqual(request.CharityActionkey, context.CharityActions.Single().CharityActionkey);
                    Assert.AreEqual(request.Charitykey, context.CharityActions.Single().Charitykey);
                    //Assert.AreEqual(request.UserKeys.ToString(), context.CharityActions.Single().UserKeys.ToString());
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
                    var users = new Collection<Guid>();
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