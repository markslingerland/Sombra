using System;
using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class ForgotPasswordResponse : Response
    {
        public Guid Secret;
        public bool Success;
    }    
}