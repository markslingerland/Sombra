using System;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class UpdateRolesRequest : Request<UpdateRolesResponse>
    {
        public Guid UserKey { get; set; }
        public Role Role { get; set; }
    }
}