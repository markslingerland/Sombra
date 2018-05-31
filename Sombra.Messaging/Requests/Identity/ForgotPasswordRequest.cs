using Sombra.Messaging.Responses.Identity;

namespace Sombra.Messaging.Requests.Identity
{
    public class ForgotPasswordRequest : Request<ForgotPasswordResponse>
    {
        public ForgotPasswordRequest(){

        }
        public ForgotPasswordRequest(string email)
        {
            Email = email;

        }
        public string Email { get; set; }
    }

}