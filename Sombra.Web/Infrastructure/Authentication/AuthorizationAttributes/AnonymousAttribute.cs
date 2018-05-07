using System;
using Microsoft.AspNetCore.Authorization;

namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AnonymousAttribute : AuthorizeAttribute
    {
        public AnonymousAttribute()
        {
            Roles = Core.Enums.Role.Default.ToString();
        }
    }
}