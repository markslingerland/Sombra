using System;
using MongoDB.Bson.Serialization.Attributes;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class LogEntry
    {
        public LogEntry(Message message, DateTime received)
        {
            MessageType = message.MessageType;
            MessageCreated = message.Created;
            MessageReceived = received;
            Message = message.ToJson();
        }

        [BsonId]
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}
