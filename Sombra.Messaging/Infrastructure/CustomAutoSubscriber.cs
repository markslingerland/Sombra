using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriber : AutoSubscriber
    {
        public CustomAutoSubscriber(IBus bus, ServiceProvider serviceProvider, string subscriptionIdPrefix) : base(bus, subscriptionIdPrefix)
        {
            AutoSubscriberMessageDispatcher = new CustomAutoSubscriberMessageDispatcher(serviceProvider);
        }
    }
}