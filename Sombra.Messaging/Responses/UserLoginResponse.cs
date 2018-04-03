using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class UserLoginResponse : Response {
        public bool Success { get;set;}
		
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public List<string> PermissionCodes { get; set; }
    }
}

