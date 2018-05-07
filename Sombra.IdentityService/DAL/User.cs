using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class User : Entity
    {
        public Guid UserKey { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        
        public virtual ICollection<Credential> Credentials { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}