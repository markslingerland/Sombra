using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using MongoDB.Driver;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class LoggingService : IConsumeAsync<Message>
    {
        private readonly IMongoCollection<LogEntry> _mongoCollection;

        public LoggingService(IMongoCollection<LogEntry> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public Task Consume(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}