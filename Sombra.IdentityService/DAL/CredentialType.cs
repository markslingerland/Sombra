using System;
using System.Collections.Generic;
using Sombra.Core;

namespace Sombra.IdentityService{
    public class CredentialType : Entity
    {
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; }
    }
}