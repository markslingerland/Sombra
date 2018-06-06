using System;
using System.Runtime.Serialization;

namespace Sombra.Messaging.Shared
{
    public class ExceptionMessage : Message
    {
        public Exception Exception { get; set; }
        public string HandlerName { get; set; }
    }
}