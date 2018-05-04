using System;
using Microsoft.AspNetCore.Authorization;

namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DefaultAttribute : AuthorizeAttribute
    {
        public DefaultAttribute()
        {
            Roles = Core.Enums.Role.Default.ToString();
        }
    }
}