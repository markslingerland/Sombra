using System;
using Microsoft.AspNetCore.Authorization;
namespace Sombra.Web.Infrastructure.Authentication.AuthorizationAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EventParticipantAttribute : AuthorizeAttribute
    {
        public EventParticipantAttribute()
        {
            Roles = Core.Enums.Role.EventParticipant.ToString();
        }
    }
}