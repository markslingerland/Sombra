using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class CreateCharityResponse : Response {
        public CreateCharityResponse() { }
        public CreateCharityResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
        public Guid CharityKey { get; set; }
        public string NameOwner { get; set; }
        public string NameCharity { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public int KVKNumber { get; set; }
        public string IBAN { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
    }

    
}

