using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class CharityResponse : Response {
        public string CharityId { get; set; }
        public string NameCharity { get; set; }
        public string NameOwner { get; set; }
    }
}

