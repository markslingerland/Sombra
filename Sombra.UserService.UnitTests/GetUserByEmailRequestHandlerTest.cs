using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService.UnitTests
{
    [TestClass]
    public class GetUserByEmailRequestHandlerTest
    {
        [TestMethod]
        public async Task GetUserByEmailRequestHandler_Handle_Returns_User()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                GetUserByEmailResponse response;
                var request = new GetUserByEmailRequest()
                {
                    EmailAddress = "john@doe.test"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test",
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new GetUserByEmailRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.UserExists);
                    Assert.AreEqual(context.Users.Single().FirstName, response.User.FirstName);
                    Assert.AreEqual(context.Users.Single().LastName, response.User.LastName);
                }
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetUserByEmailRequestHandler_Handle_Returns_NotExists()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                GetUserByEmailResponse response;
                var request = new GetUserByEmailRequest()
                {
                    EmailAddress = "ellen@doe.test"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test",
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new GetUserByEmailRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.UserExists);
                Assert.IsNull(response.User);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }
    }
}