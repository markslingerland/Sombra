using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;
using Sombra.Messaging.Events;

namespace Sombra.Messaging.Infrastructure
{
    public class PublishOnlineEventHandler : IAsyncEventHandler<PublishOnlineEvent>
    {
        private readonly IBus _bus;
        public PublishOnlineEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(PublishOnlineEvent message)
        {
            ExtendedConsole.Log("PublishOnlineEvent received");
            await _bus.PublishAsync(new OnlineEvent
            {
                ServiceName = Assembly.GetEntryAssembly().GetName().Name
            });
        }
    }
}