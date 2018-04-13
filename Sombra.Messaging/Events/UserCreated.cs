using System;

namespace Sombra.Messaging.Events
{
    public class UserCreated : Event
    {
        public Guid UserKey { get; set; }
    }
}