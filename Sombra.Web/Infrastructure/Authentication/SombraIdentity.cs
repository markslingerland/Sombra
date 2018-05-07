using System;
using System.Collections.Generic;
using System.Security.Claims;
using Sombra.Core.Enums;

namespace Sombra.Web.Infrastructure.Authentication
{
    public sealed class SombraIdentity : ClaimsIdentity
    {
        public SombraIdentity(IEnumerable<Claim> claims, Guid userKey, IEnumerable<Role> roles, string authenticationType) : base(claims, authenticationType)
        {
            Roles = roles;
            UserKey = userKey;
        }

        public Guid UserKey { get; }
        public IEnumerable<Role> Roles { get; }
    }
}