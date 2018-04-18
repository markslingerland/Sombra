using System;
using System.Collections.Generic;
using System.Security.Claims;
using Sombra.Core.Enums;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class SombraIdentity : ClaimsIdentity
    {
        public SombraIdentity(IEnumerable<Claim> claims, Guid userKey, IEnumerable<Role> roles, IEnumerable<Permission> permissions, string authenticationType) : base(claims, authenticationType)
        {
            Roles = roles;
            Permissions = permissions;
            UserKey = userKey;
        }

        public Guid UserKey { get; }
        public IEnumerable<Role> Roles { get; }
        public IEnumerable<Permission> Permissions { get; }
    }
}