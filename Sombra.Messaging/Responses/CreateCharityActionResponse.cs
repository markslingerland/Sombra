using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class CreateCharityActionResponse : Response
    {
        public CreateCharityActionResponse() { }

        public CreateCharityActionResponse(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}