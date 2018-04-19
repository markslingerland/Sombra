using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService.UnitTests
{
    [TestClass]
    public class GetUserByKeyRequestHandlerTest
    {
        [TestMethod]
        public async Task GetUserByKeyRequestHandler_Handle_Returns_User()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                GetUserByKeyResponse response;
                var request = new GetUserByKeyRequest
                {
                    UserKey = Guid.NewGuid()
                };

                using (var context = new UserContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        UserKey = request.UserKey,
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options))
                {
                    var handler = new GetUserByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                using (var context = new UserContext(options))
                {
                    Assert.IsTrue(response.UserExists);
                    Assert.AreEqual(context.Users.Single().FirstName, response.FirstName);
                    Assert.AreEqual(context.Users.Single().LastName, response.LastName);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public async Task GetUserByKeyRequestHandler_Handle_Returns_NotExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                GetUserByKeyResponse response;
                var request = new GetUserByKeyRequest();

                using (var context = new UserContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        UserKey = Guid.NewGuid(),
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options))
                {
                    var handler = new GetUserByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.UserExists);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}