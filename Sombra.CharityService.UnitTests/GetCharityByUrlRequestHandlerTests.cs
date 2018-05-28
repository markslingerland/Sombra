using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using Sombra.Infrastructure;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class GetCharityByUrlRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityByUrlRequest_Handle_Returns_Charity()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid(),
                        Name = "testNameCharity",
                        OwnerUserName = "testNAmeOwner",
                        Email = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = "",
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test",
                        Url = "test",
                        IsApproved = true
                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityByUrlRequest()
                {
                    Url = "TEST"
                };

                GetCharityByUrlResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(response.Url.ToLower(), request.Url.ToLower());
                    Assert.AreEqual(response.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(response.Name, context.Charities.Single().Name);
                    Assert.AreEqual(response.OwnerUserName, context.Charities.Single().OwnerUserName);
                    Assert.AreEqual(response.Email, context.Charities.Single().Email);
                    Assert.AreEqual(response.Category, context.Charities.Single().Category);
                    Assert.AreEqual(response.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(response.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(response.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(response.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityByUrlRequest_Handle_Returns_Null()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid(),
                        Name = "testNameCharity",
                        OwnerUserName = "testNAmeOwner",
                        Email = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = "",
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test",
                        Url = "test",
                        IsApproved = true
                    };

                    context.Add(charity);
                    context.SaveChanges();

                }

                var request = new GetCharityByUrlRequest
                {
                    Url = "WeirdUrl"
                };

                GetCharityByUrlResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.IsFalse(response.Success);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityByUrlRequest_Handle_Returns_Null_NotApproved()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid(),
                        Name = "testNameCharity",
                        OwnerUserName = "testNAmeOwner",
                        Email = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = "",
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test",
                        Url = "test"
                    };

                    context.Add(charity);
                    context.SaveChanges();

                }

                var request = new GetCharityByUrlRequest
                {
                    Url = "WeirdUrl"
                };

                GetCharityByUrlResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByUrlRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.IsFalse(response.Success);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}