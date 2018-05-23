using System;
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
            UserContext.OpenInMemoryConnection();

            try
            {
                UserEmailExistsResponse response;
                var request = new UserEmailExistsRequest
                {
                    EmailAddress = "john@doe.test"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new UserEmailExistsRequestHandler(context);
                    response = await handler.Handle(request);
                }

                Assert.IsTrue(response.EmailExists);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserEmailExistsRequestHandler_Handle__Returns_EmailNotExists()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                UserEmailExistsResponse response;
                var request = new UserEmailExistsRequest
                {
                    EmailAddress = "ellen@doe.test"
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new UserEmailExistsRequestHandler(context);
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.EmailExists);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UserEmailExistsRequestHandler_Handle__Returns_EmailNotExistsForUser()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                UserEmailExistsResponse response;
                var key = Guid.NewGuid();

                var request = new UserEmailExistsRequest
                {
                    EmailAddress = "john@doe.test",
                    CurrentUserKey = key
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        EmailAddress = "john@doe.test",
                        UserKey = key
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new UserEmailExistsRequestHandler(context);
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.EmailExists);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }
    }
}