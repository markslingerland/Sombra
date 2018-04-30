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
    public class GetUserByEmailRequestHandlerTest
    {
        [TestMethod]
        public async Task GetUserByEmailRequestHandler_Handle_Returns_User()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                GetUserByEmailResponse response;
                var request = new GetUserByEmailRequest()
                {
                    EmailAddress = "john@doe.test"
                };

                using (var context = new UserContext(options, false))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test",
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options, false))
                {
                    var handler = new GetUserByEmailRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                using (var context = new UserContext(options, false))
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
        public async Task GetUserByEmailRequestHandler_Handle_Returns_NotExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite(connection)
                    .Options;

                GetUserByEmailResponse response;
                var request = new GetUserByEmailRequest()
                {
                    EmailAddress = "ellen@doe.test"
                };

                using (var context = new UserContext(options, false))
                {
                    context.Database.EnsureCreated();
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test",
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = new UserContext(options, false))
                {
                    var handler = new GetUserByEmailRequestHandler(context, Helper.GetMapper());
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