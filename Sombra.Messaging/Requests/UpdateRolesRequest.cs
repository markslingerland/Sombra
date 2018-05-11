using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class UpdateRolesRequest : Request<UpdateRolesResponse>
    {
        public Guid UserKey { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}