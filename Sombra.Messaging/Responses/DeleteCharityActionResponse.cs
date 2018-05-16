using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class DeleteCharityActionResponse : Response
    {
        public DeleteCharityActionResponse() { }
        public DeleteCharityActionResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
    }
}