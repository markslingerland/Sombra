using Sombra.Core.Enums;
using System;

namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByKeyResponse : Response
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
        public string Anbi { get; set; }
        public string Description { get; set; }
        public string ThankYou { get; set; }
    }
}