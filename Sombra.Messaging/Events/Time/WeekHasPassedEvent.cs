using System;

namespace Sombra.Messaging.Events.Time
{
    public class WeekHasPassedEvent : TimeHasPassedEvent
    {
        public WeekHasPassedEvent() { }
        public WeekHasPassedEvent(DateTime dateTimeStamp) : base(dateTimeStamp) { }
    }
}