using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Sombra.Core.Enums;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class SombraPrincipal : ClaimsPrincipal
    {
        public SombraPrincipal(SombraIdentity identity) : base(identity)
        {
            Roles = identity.Roles;
            Permissions = identity.Permissions;
            Key = identity.UserKey;
        }

        public Guid Key { get; }
        public IEnumerable<Role> Roles { get; }
        public IEnumerable<Permission> Permissions { get; }
        public bool HasPermission(Permission permission) => Permissions.Contains(permission);
        public bool IsInRole(Role role) => Roles.Contains(role);
    }
}