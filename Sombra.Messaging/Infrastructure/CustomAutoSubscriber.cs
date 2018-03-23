using System;
using System.Threading.Tasks;
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

        public override void SubscribeAsync(params Type[] consumerTypes)
        {
            var genericBusSubscribeMethod = GetSubscribeMethodOfBus(nameof(IBus.SubscribeAsync), typeof(Func<,>));
            var subscriptionInfos = GetSubscriptionInfos(consumerTypes, typeof(IAsyncEventHandler<>));
            Type SubscriberTypeFromMessageTypeDelegate(Type messageType) => typeof(Func<,>).MakeGenericType(messageType, typeof(Task));
            InvokeMethods(subscriptionInfos, DispatchAsyncMethodName, genericBusSubscribeMethod, SubscriberTypeFromMessageTypeDelegate);
        }
    }
}