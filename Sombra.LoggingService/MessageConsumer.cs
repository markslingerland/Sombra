using System;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using MongoDB.Driver;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class MessageConsumer : IConsumeAsync<Message>
    {
        private readonly IMongoCollection<LogEntry> _mongoCollection;

        public MessageConsumer(IMongoCollection<LogEntry> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public async Task Consume(Message message)
        {
            await _mongoCollection.InsertOneAsync(new LogEntry(message, DateTime.UtcNow)).ConfigureAwait(false);
        }
    }
}