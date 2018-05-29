using System;

namespace Sombra.Messaging.Events.Time
{
    public class DayHasPassedEvent : Event
    {
        public DayHasPassedEvent() { }

        public DayHasPassedEvent(DateTime dateTimeStamp)
        {
            DateTimeStamp = dateTimeStamp;
        }

        public DateTime DateTimeStamp { get; set; }
    }
}