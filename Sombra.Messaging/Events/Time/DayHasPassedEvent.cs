using System;

namespace Sombra.Messaging.Events.Time
{
    public class DayHasPassedEvent : TimeHasPassedEvent
    {
        public DayHasPassedEvent() { }
        public DayHasPassedEvent(DateTime dateTimeStamp) : base(dateTimeStamp) { }
    }
}