using System;

namespace Sombra.Messaging.Shared
{
    public class ExceptionMessage : Message
    {
        public ExceptionMessage() { }

        public ExceptionMessage(Exception exception, bool isHandled)
        {
            Exception = exception;
            IsHandled = isHandled;
        }

        public ExceptionMessage(Exception exception, bool isHandled, string handlerName) : this(exception, isHandled)
        {
            HandlerName = handlerName;
        }

        public Exception Exception { get; set; }
        public string HandlerName { get; set; }
        public bool IsHandled { get; set; }
    }
}