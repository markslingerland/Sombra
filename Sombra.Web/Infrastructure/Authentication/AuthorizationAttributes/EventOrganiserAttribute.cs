using System;
using Microsoft.AspNetCore.Authorization;
namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EventOrganiserAttribute : AuthorizeAttribute
    {
        public EventOrganiserAttribute()
        {
            Roles = Core.Enums.Role.EventOrganiser.ToString();
        }
    }
}