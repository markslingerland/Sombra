using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class UserLoginResponse : Message {
        public bool Success { get;set;}
		
        public User User { get; set; }
    }
}

