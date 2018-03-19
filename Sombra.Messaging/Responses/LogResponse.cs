using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class LogResponse : Message
    {
        public IEnumerable<Log> Logs { get; set; }
    }

    public class Log
    {
        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}