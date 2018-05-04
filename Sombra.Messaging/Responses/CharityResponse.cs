using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class CharityResponse : Response {
        public CharityResponse() { }
        public CharityResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
        public string CharityId { get; set; }
        public string NameCharity { get; set; }
        public string NameOwner { get; set; }
    }

    
}

