using System;
using Microsoft.AspNetCore.Authorization;
namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CharityUserAttribute : AuthorizeAttribute
    {
        public CharityUserAttribute()
        {
            Roles = Core.Enums.Role.CharityUser.ToString();
        }
    }
}