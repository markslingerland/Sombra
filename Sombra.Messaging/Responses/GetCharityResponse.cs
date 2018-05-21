using Sombra.Core.Enums;
using System;

namespace Sombra.Messaging.Responses
{
    public class GetCharityResponse : Response
    {
        public bool Success { get; set; }
        public Guid CharityKey { get; set; }
        public string NameOwner { get; set; }
        public string CharityName { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public string KVKNumber { get; set; }
        public string IBAN { get; set; }
    }
}

