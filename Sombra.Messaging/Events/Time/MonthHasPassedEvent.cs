using System;

namespace Sombra.Messaging.Events.Time
{
    public class MonthHasPassedEvent : TimeHasPassedEvent
    {
        public MonthHasPassedEvent() { }
        public MonthHasPassedEvent(DateTime dateTimeStamp) : base(dateTimeStamp) { }
    }
}