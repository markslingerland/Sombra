using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using System;

namespace Sombra.CharityService.DAL
{
    public class CharityEntity : Entity
    {
        // TODO at more data relevant for charity
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
