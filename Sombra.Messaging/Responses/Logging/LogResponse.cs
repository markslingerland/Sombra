using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses.Logging
{
    public class LogResponse : Response
    {
        public List<Log> Logs { get; set; }
    }

    public class Log
    {
        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}