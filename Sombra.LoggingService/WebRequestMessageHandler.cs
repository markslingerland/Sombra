using System.Threading.Tasks;
using MongoDB.Driver;
using Sombra.Core;
using Sombra.Messaging.Shared;

namespace Sombra.LoggingService
{
    public class WebRequestMessageHandler
    {
        private readonly IMongoCollection<WebRequestEntry> _logCollection;
        private static readonly string _mongoCollection = "webrequests";

        public WebRequestMessageHandler(IMongoDatabase database)
        {
            _logCollection = database.GetCollection<WebRequestEntry>(_mongoCollection);
        }

        public async Task HandleAsync(WebRequest message)
        {
            ExtendedConsole.Log($"{message.GetType().Name} received");
            await _logCollection.InsertOneAsync(new WebRequestEntry(message));
        }
    }
}