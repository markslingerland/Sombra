using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ForgotPasswordRequest : Request<ForgotPasswordResponse>
    {
        public ForgotPasswordRequest(string email)
        {
            this.Email = email;

        }
        public string Email { get; set; }
    }

}