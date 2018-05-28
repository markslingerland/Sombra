using System;

namespace Sombra.Messaging.Events
{
    public class MonthHasPassedEvent : Event
    {
        public MonthHasPassedEvent() { }

        public MonthHasPassedEvent(DateTime dateTimeStamp)
        {
            DateTimeStamp = dateTimeStamp;
        }

        public DateTime DateTimeStamp { get; set; }
    }
}