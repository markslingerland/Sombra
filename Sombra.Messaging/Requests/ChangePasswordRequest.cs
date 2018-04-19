using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ChangePasswordRequest : Request<ChangePasswordResponse>
    {
        public ChangePasswordRequest(string password, Guid securityToken)
        {
            Password = password;
            SecurityToken = securityToken;

        }
        public string Password { get; set; }
        public Guid SecurityToken { get; set; }
    }
}