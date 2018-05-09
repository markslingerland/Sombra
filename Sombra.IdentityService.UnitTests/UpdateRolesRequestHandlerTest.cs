using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Role = Sombra.Core.Enums.Role;

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
                    Roles = new List<Role> { Role.CharityOwner }
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        UserKey = request.UserKey,
                        Name = "John Doe"
                    };

                    var role1 = new DAL.Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = Role.Donator,
                        User = user
                    };

                    var role2 = new DAL.Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = Role.CharityUser,
                        User = user
                    };

                    context.Roles.Add(role1);
                    context.Roles.Add(role2);
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
                    CollectionAssert.AreEquivalent(request.Roles.ToList(), context.Roles.Select(r => r.RoleName).ToList());
                    CollectionAssert.AreEquivalent(request.Roles.ToList(), response.Roles.ToList());
                    Assert.IsTrue(response.Success);
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
                    Roles = new List<Role> { Role.CharityOwner }
                };

                using (var context = AuthenticationContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        UserKey = Guid.NewGuid(),
                        Name = "John Doe"
                    };

                    var role = new DAL.Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = Role.Donator,
                        User = user
                    };

                    context.Roles.Add(role);
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
                    Assert.AreEqual(Role.Donator, context.Roles.Single().RoleName);
                    Assert.AreEqual(response.ErrorType, ErrorType.InvalidUserKey);
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                AuthenticationContext.CloseInMemoryConnection();
            }
        }
    }
}