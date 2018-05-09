using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Threading.Tasks;

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

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new CharityEntity
                    {
                        CharityId = "1",
                        NameCharity = "testCharity",
                        NameOwner = "testOwner"

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityRequest()
                {
                    CharityId = "1",
                    NameCharity = "testCharity",
                    NameOwner = "testOwner"

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
                    Assert.AreEqual(response.CharityId, "1");
                    Assert.AreEqual(response.NameCharity, "testCharity");
                    Assert.AreEqual(response.NameOwner, "testOwner");
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
                        CharityId = "1",
                        NameCharity = "testCharity",
                        NameOwner = "testOwner"

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