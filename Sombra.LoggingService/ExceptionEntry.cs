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
            MessageCreated = message.MessageCreated;
            MessageReceived = received;
            IsHandled = message.IsHandled;
            ExceptionType = message.Exception.GetType().Name;
            Exception = message.Exception.ToString();
        }

        public string ExceptionType { get; set; }
        public bool IsHandled { get; set; }
        public string Exception { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}