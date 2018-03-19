using System;
using System.Collections.Generic;
using Sombra.Core;

namespace Sombra.IdentityService{
    public class User : Entity
    {
    public string Name { get; set; }
    public DateTime Created { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; }
    }
}