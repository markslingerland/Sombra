using System;
using Sombra.Infrastructure.DAL.Mongo;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class LogEntry : DocumentEntity
    {
        public LogEntry(IMessage message, DateTime received)
        {
            MessageType = message.MessageType;
            MessageCreated = message.MessageCreated;
            MessageReceived = received;
            Message = message.ToJson();
        }

        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}