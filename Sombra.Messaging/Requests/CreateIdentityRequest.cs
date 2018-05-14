using System;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class CreateIdentityRequest : Request<CreateIdentityResponse>
    {
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        public CredentialType CredentialType { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}