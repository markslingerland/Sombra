using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class CredentialType : Entity
    {
        public Core.Enums.CredentialType Name { get; set; }
        public int? Position { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
    }
}