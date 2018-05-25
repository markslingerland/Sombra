using System;

namespace Sombra.Messaging.Events
{
    public class WeekHasPassedEvent : Event
    {
        public WeekHasPassedEvent() { }

        public WeekHasPassedEvent(DateTime dateTimeStamp)
        {
            DateTimeStamp = dateTimeStamp;
        }

        public DateTime DateTimeStamp { get; set; }
    }
}