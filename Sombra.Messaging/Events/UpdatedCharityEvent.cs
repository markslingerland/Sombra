using Sombra.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Messaging.Events
{
    public class UpdatedCharityEvent : Event
    {
        public Guid CharityKey { get; set; }
        public Guid OwnerUserKey { get; set; }
        public string OwnerUserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
        public int KVKNumber { get; set; }
        public string IBAN { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
    }
}
