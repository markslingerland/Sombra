using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Events.CharityAction
{
    public class CharityActionUpdatedEvent : Event
    {
        public Guid CharityActionKey { get; set; }
        public Guid CharityKey { get; set; }
        public List<UserKey> UserKeys { get; set; }
        public string CharityName { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }
}
