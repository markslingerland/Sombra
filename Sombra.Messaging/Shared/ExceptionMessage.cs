using System;

namespace Sombra.Messaging.Shared
{
    public class ExceptionMessage : Message
    {
        public ExceptionMessage() { }

        public ExceptionMessage(Exception exception)
        {
            Exception = exception;
        }

        public ExceptionMessage(Exception exception, string handlerName) : this(exception)
        {
            HandlerName = handlerName;
        }

        public Exception Exception { get; set; }
        public string HandlerName { get; set; }
    }
}