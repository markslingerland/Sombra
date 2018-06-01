using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sombra.Core;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class MessageHandler
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private static readonly string _mongoCollection = "rabbitmq";

        public MessageHandler(IMongoDatabase database)
        {
            _logCollection = database.GetCollection<LogEntry>(_mongoCollection);
        }

        public async Task HandleAsync(IMessage message)
        {
            ExtendedConsole.Log($"{message.GetType().Name} received");
            await _logCollection.InsertOneAsync(new LogEntry(message, DateTime.UtcNow));
        }
    }
}