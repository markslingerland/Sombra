using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sombra.Core;
using Sombra.Messaging;
using Sombra.Messaging.Shared;

namespace Sombra.LoggingService
{
    public class ExceptionMessageHandler
    {
        private readonly IMongoCollection<ExceptionEntry> _logCollection;
        private static readonly string _mongoCollection = "exceptions";

        public ExceptionMessageHandler(IMongoDatabase database)
        {
            _logCollection = database.GetCollection<ExceptionEntry>(_mongoCollection);
        }

        public async Task HandleAsync(ExceptionMessage message)
        {
            ExtendedConsole.Log($"{message.GetType().Name} received");
            await _logCollection.InsertOneAsync(new ExceptionEntry(message, DateTime.UtcNow));
        }
    }
}