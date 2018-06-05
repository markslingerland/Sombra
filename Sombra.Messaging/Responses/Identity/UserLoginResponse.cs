using System;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class UserLoginResponse : CrudResponse
    {
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
    }
}