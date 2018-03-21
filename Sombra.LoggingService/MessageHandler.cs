using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sombra.Messaging;
using Sombra.Messaging.Infrastructure;

namespace Sombra.LoggingService
{
    public class MessageHandler : IAsyncMessageHandler<Message>
    {
        private readonly IMongoCollection<LogEntry> _mongoCollection;

        public MessageHandler(IMongoCollection<LogEntry> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public async Task Consume(Message message)
        {
            Console.WriteLine($"Message received. Type: {message.MessageType}");
            await _mongoCollection.InsertOneAsync(new LogEntry(message, DateTime.UtcNow)).ConfigureAwait(false);
        }
    }
}