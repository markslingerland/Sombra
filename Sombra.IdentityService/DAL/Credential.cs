using System;
using System.Collections.Generic;
using Sombra.Core;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService{
    public class Credential : Entity
    {
    public Guid UserId { get; set; }
    public Guid CredentialTypeId { get; set; }
    public string Identifier { get; set; }
    public string Secret { get; set; }

    public virtual User User { get; set; }
    public virtual CredentialType CredentialType { get; set; }
    }
}