using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class LoggingService : IConsumeAsync<Message>
    {
        public Task Consume(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}