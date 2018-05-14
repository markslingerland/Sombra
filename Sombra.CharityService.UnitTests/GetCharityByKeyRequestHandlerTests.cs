using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class GetCharityByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityByKeyRequest_Handle_Returns_Charity()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                var key = Guid.NewGuid();
                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new CharityEntity
                    {
                        CharityKey = key,
                        NameCharity = "testNameCharity",
                        NameOwner = "testNAmeOwner",
                        EmailCharity = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = 0,
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test"

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityRequest()
                {
                    CharityKey = key,
                    NameCharity = "testNameCharity",
                    NameOwner = "testNAmeOwner",
                    EmailCharity = "test@test.com",
                    Category = Core.Enums.Category.None,
                    KVKNumber = 0,
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "Test"

                };

                GetCharityResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityContext.GetInMemoryContext())
                {
                    // TODO Fix unit test problem
                    Assert.AreEqual(request.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(request.NameCharity, context.Charities.Single().NameCharity);
                    Assert.AreEqual(request.NameOwner, context.Charities.Single().NameOwner);
                    Assert.AreEqual(request.EmailCharity, context.Charities.Single().EmailCharity);
                    Assert.AreEqual(request.Category, context.Charities.Single().Category);
                    Assert.AreEqual(request.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(request.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(request.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(request.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityByKeyRequest_Handle_Returns_Null()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new CharityEntity
                    {
                        CharityKey = Guid.NewGuid(),
                        NameCharity = "testNameCharity",
                        NameOwner = "testNAmeOwner",
                        EmailCharity = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = 0,
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test"

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityRequest();

                GetCharityResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}