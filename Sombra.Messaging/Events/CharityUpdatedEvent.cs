using Sombra.Core.Enums;
using System;

namespace Sombra.Messaging.Events
{
    public class CharityUpdatedEvent : Event
    {
        public Guid CharityKey { get; set; }
        public string OwnerUserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
        public string KVKNumber { get; set; }
        public string IBAN { get; set; }
<<<<<<< HEAD
=======
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public string Url { get; set; }
>>>>>>> Charity
    }
}
