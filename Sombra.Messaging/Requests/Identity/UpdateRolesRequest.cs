using System;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.Messaging.Requests.Identity
{
    public class UpdateRolesRequest : Request<UpdateRolesResponse>
    {
        public Guid UserKey { get; set; }
        public Role Role { get; set; }
    }
}