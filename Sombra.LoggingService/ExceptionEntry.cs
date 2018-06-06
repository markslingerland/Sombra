using System;
using Sombra.Infrastructure.DAL.Mongo;
using Sombra.Messaging;
using Sombra.Messaging.Shared;

namespace Sombra.LoggingService
{
    public class ExceptionEntry : DocumentEntity
    {
        public ExceptionEntry(ExceptionMessage message, DateTime received)
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