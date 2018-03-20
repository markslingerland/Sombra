using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriber : AutoSubscriber
    {
        public CustomAutoSubscriber(IBus bus, System.IServiceProvider serviceProvider, string subscriptionIdPrefix) : base(bus, subscriptionIdPrefix)
        {
            AutoSubscriberMessageDispatcher = new CustomAutoSubscriberMessageDispatcher(serviceProvider);
        }
    }
}