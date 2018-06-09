using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.Core.Extensions;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService.UnitTests
{
    [TestClass]
    public class UpdateRolesRequestHandlerTest
    {
        [TestMethod]
        public async Task UpdateRolesRequestHandler_Returns_Success()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                UpdateRolesResponse response;
                var request = new UpdateRolesRequest
                {
                    UserKey = Guid.NewGuid(),
                    Role = Role.CharityOwner
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        UserKey = request.UserKey,
                        Name = "John Doe",
                        Role = Role.CharityUser | Role.Donator
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UpdateRolesRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.Role, response.Role);
                    Assert.IsTrue(context.Users.Single().Role.OnlyHasFlag(Role.CharityOwner));
                    Assert.IsTrue(response.IsSuccess);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task UpdateRolesRequestHandler_Handle_ReturnsInvalidUserKey()
        {
            AuthenticationContext.OpenInMemoryConnection();
            try
            {
                UpdateRolesResponse response;
                var request = new UpdateRolesRequest
                {
                    UserKey = Guid.NewGuid(),
                    Role = Role.CharityOwner
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        UserKey = Guid.NewGuid(),
                        Name = "John Doe",
                        Role = Role.Donator
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    var handler = new UpdateRolesRequestHandler(context);
                    response = await handler.Handle(request);
                }

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    Assert.IsTrue(context.Users.Single().Role.OnlyHasFlag(Role.Donator));
                    Assert.AreEqual(response.ErrorType, ErrorType.InvalidKey);
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}