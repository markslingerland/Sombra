using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Sombra.Core.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.LoggingService
{
    public class LogRequestHandler : IAsyncRequestHandler<LogRequest, LogResponse>
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private readonly IMapper _mapper;

        public LogRequestHandler(IMongoCollection<LogEntry> logCollection, IMapper mapper)
        {
            _logCollection = logCollection;
            _mapper = mapper;
        }

        public async Task<LogResponse> Handle(LogRequest message)
        {
            Console.WriteLine("LogRequest received");
            Expression<Func<LogEntry, bool>> filter = l => true;

            if (message.From.HasValue) filter = filter.And(l => l.MessageCreated.CompareTo(message.From.Value) >= 0);
            if (message.To.HasValue) filter = filter.And(l => l.MessageCreated.CompareTo(message.To.Value) <= 0);
            if (message.MessageTypes?.Any() != null) filter = filter.And(l => message.MessageTypes.Select(t => t.FullName).Contains(l.MessageType));

            var logs = await _logCollection.Find(filter).Project(l => _mapper.Map<Log>(l)).ToListAsync().ConfigureAwait(false);

            return new LogResponse
            {
                Logs = logs
            };
        }
    }
}
