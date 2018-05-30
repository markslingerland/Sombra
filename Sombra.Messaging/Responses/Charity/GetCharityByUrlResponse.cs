using System;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByUrlResponse : Response
    {
        public bool Success { get; set; }

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