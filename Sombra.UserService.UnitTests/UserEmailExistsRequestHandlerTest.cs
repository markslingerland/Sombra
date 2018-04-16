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
    public class UserEmailExistsRequestHandlerTest
    {
        [TestMethod]
        public async Task UserEmailExistsRequestHandler_Handle_Returns_EmailExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                UserEmailExistsResponse response;
                var request = new UserEmailExistsRequest
                {
                    EmailAddress = "john@doe.test"
                };

                using (var context = new UserContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options))
                {
                    var handler = new UserEmailExistsRequestHandler(context);
                    response = await handler.Handle(request);
                }

                Assert.IsTrue(response.EmailExists);
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public async Task UserEmailExistsRequestHandler_Handle__Returns_EmailNotExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                UserEmailExistsResponse response;
                var request = new UserEmailExistsRequest
                {
                    EmailAddress = "ellen@doe.test"
                };

                using (var context = new UserContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options))
                {
                    var handler = new UserEmailExistsRequestHandler(context);
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.EmailExists);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}