using System;
using Microsoft.AspNetCore.Authorization;
namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CharityOwnerAttribute : AuthorizeAttribute
    {
        public CharityOwnerAttribute()
        {
            Roles = Core.Enums.Role.CharityOwner.ToString();
        }
    }
}