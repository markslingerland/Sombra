using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class User : Entity
    {
        public Guid UserKey { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; } = false;
        public string ActivationToken { get; set; }
        public DateTime ActivationTokenExpirationDate { get; set; }
        
        public virtual ICollection<Credential> Credentials { get; set; }
        public Role Role { get; set; }
    }
}