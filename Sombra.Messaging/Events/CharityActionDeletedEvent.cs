using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Events
{
    public class CharityActionDeletedEvent
    {
        public Guid CharityActionKey { get; set; }
    }
}
