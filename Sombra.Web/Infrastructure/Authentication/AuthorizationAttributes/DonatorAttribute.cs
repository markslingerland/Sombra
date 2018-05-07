using System;
using Microsoft.AspNetCore.Authorization;
namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DonatorAttribute : AuthorizeAttribute
    {
        public DonatorAttribute()
        {
            Roles = Core.Enums.Role.Donator.ToString();
        }
    }
}