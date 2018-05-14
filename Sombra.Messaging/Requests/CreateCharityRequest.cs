using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class CreateCharityRequest : Request<CreateCharityResponse>
    {
        public string CharityId { get; set; }
        public string NameOwner { get; set; }
        public string NameCharity { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public int KVKNumber { get; set; }
        public string IBAN { get; set; }
    }
}