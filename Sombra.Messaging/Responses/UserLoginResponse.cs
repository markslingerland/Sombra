using System.Collections.Generic;

namespace Sombra.Messaging{
    public class UserLoginResponse : Message {
        public bool Success { get;set;}
		public int UserId {get;set;}    
		public string Username {get;set;}
		public IEnumerable<string> Roles {get;set;}
    }
}

