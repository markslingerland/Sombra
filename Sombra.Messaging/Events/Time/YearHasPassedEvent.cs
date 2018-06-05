using System;

namespace Sombra.Messaging.Events.Time
{
    public class YearHasPassedEvent : TimeHasPassedEvent
    {
        public YearHasPassedEvent() { }
        public YearHasPassedEvent(DateTime dateTimeStamp) : base(dateTimeStamp) { }
    }
}