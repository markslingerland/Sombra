using System;

namespace Sombra.Messaging.Events.Time
{
    public abstract class TimeHasPassedEvent : Event
    {
        protected TimeHasPassedEvent() { }

        protected TimeHasPassedEvent(DateTime dateTimeStamp)
        {
            DateTimeStamp = dateTimeStamp;
        }

        public DateTime DateTimeStamp { get; set; }
    }
}