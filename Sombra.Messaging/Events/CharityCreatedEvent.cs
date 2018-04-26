using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Messaging.Events
{
    public class CharityCreatedEvent : Event
    {
        public string CharityId { get; set; }
        public string NameCharity { get; set; }
        public string NameOwner { get; set; }
    }
}
