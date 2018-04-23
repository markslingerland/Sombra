using System;
using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class ForgotPasswordResponse : Response
    {
        public string Secret;
        public bool Success;
    }    
}