using System;
using System.Collections.Generic;
using Sombra.Core;

namespace Sombra.IdentityService{
    public class Credential : Entity
    {
    public int UserId { get; set; }
    public int CredentialTypeId { get; set; }
    public string Identifier { get; set; }
    public string Secret { get; set; }

    public virtual User User { get; set; }
    public virtual CredentialType CredentialType { get; set; }
    }
}