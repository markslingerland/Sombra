using System;
using System.Collections.Generic;
using System.Security.Claims;
using Sombra.Core.Enums;

namespace Sombra.Web.Infrastructure.Authentication
{
    public sealed class SombraIdentity : ClaimsIdentity
    {
        public SombraIdentity(IEnumerable<Claim> claims, Guid userKey, Role role, string authenticationType) : base(claims, authenticationType)
        {
            Role = role;
            UserKey = userKey;
        }

        public Guid UserKey { get; }
        public Role Role { get; }
    }
}