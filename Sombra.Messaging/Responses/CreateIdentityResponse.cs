﻿using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class CreateIdentityResponse : Response
    {
        public bool Success { get; set; }
        public string ActivationToken { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}