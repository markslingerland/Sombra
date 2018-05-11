using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ChangePasswordRequest : Request<ChangePasswordResponse>
    {
        public ChangePasswordRequest(){
            
        }
        public ChangePasswordRequest(string password, string securityToken)
        {
            Password = password;
            SecurityToken = securityToken;

        }
        public string Password { get; set; }
        public string SecurityToken { get; set; }
    }
}