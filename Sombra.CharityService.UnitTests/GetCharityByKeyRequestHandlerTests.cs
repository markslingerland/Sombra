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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<CharityContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CharityContext(options))
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
                var request = new CharityRequest()
                {
                    CharityId = "1",
                    NameCharity = "testCharity",
                    NameOwner = "testOwner"

                };

                CharityResponse response;

                //Act
                using (var context = new CharityContext(options))
                {
                    var handler = new GetCharityByKeyRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new CharityContext(options))
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
                connection.Close();
            }
        }

        [TestMethod]
        public async Task GetCharityByKeyRequest_Handle_Returns_Null()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<CharityContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CharityContext(options))
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
                var request = new CharityRequest();

                CharityResponse response;

                //Act
                using (var context = new CharityContext(options))
                {
                    var handler = new GetCharityByKeyRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new CharityContext(options))
                {
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}