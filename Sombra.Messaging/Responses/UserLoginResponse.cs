using System;
using System.Collections.Generic;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses{
    public class UserLoginResponse : Response {
        public bool Success { get;set;}
		
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public List<Role> Roles { get; set; }
    }
}

