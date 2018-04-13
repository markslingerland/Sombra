using System;

namespace Sombra.Messaging.Events
{
    public class UserUpdated : Event
    {
        public Guid UserKey { get; set; }
    }
}