﻿using System;
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
            Identity = identity;
        }

        public new SombraIdentity Identity { get; }
        public Guid Key => Identity.UserKey;
        public IEnumerable<Role> Roles => Identity.Roles;
        public IEnumerable<Permission> Permissions => Identity.Permissions;

        public bool HasPermission(Permission permission) => Permissions.Contains(permission);
        public bool IsInRole(Role role) => Roles.Contains(role);
    }
}