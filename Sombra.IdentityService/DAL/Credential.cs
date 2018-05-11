using System;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class Credential : Entity
    {
        public Guid UserId { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
        public string SecurityToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Core.Enums.CredentialType CredentialType { get; set; }

        public virtual User User { get; set; }
    }
}