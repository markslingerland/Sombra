using System;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class UserLoginResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
    }
}