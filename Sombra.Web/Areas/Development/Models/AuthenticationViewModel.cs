using System;
using System.Collections.Generic;

namespace Sombra.Web.Areas.Development.Models{
    public class AuthenticationViewModel {
        public bool Success { get;set;}
		
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> PermissionCodes { get; set; }
    }
}