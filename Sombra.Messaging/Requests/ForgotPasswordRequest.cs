using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
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