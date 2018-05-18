using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;

namespace Sombra.Messaging.Requests
{
    public class CreateCharityRequest : Request<CreateCharityResponse>
    {
        public Guid CharityKey { get; set; }
        public Guid OwnerUserKey { get; set; }
        public string OwnerUserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
        public string KVKNumber { get; set; }
        public string IBAN { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public string Url { get; set; }
    }
}