using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriber : AutoSubscriber
    {
        public CustomAutoSubscriber(IBus bus, IAutoSubscriberMessageDispatcher messageDispatcher, string subscriptionIdPrefix) : base(bus, subscriptionIdPrefix)
        {
            AutoSubscriberMessageDispatcher = messageDispatcher;
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