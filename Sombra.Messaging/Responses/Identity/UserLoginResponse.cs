using System;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class UserLoginResponse : CrudResponse<UserLoginResponse>
    {
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        public string EncrytedPassword { get; set; }
    }
}