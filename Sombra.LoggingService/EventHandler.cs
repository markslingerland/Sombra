using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sombra.Core;
using Sombra.Messaging;
using Sombra.Messaging.Infrastructure;

namespace Sombra.LoggingService
{
    public class EventHandler<TMessage> : IAsyncEventHandler<TMessage>
        where TMessage: class, IEvent
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private static readonly string _mongoCollection = "rabbitmq";

        public EventHandler(IMongoDatabase database)
        {
            _logCollection = database.GetCollection<LogEntry>(_mongoCollection);
        }

        public async Task Consume(TMessage message)
        {
            ExtendedConsole.Log($"Message received. Type: {message.MessageType}");
            await _logCollection.InsertOneAsync(new LogEntry(message, DateTime.UtcNow));
        }
    }
}